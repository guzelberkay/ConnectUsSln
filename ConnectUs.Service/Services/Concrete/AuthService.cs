using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Entity.Model;
using ConnectUs.Service.Services.Abstractions;
using static ConnectUs.Core.Utilities.CodeGenerator;

namespace ConnectUs.Service.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly PasswordEncoder _passwordEncoder;
        private readonly JwtTokenManager _jwtTokenManager;
        private readonly IEmailService _emailService;

        public AuthService(IAuthRepository authRepository, PasswordEncoder passwordEncoder, JwtTokenManager jwtTokenManager, IEmailService emailService)
        {
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            _passwordEncoder = passwordEncoder ?? throw new ArgumentNullException(nameof(passwordEncoder));
            _jwtTokenManager = jwtTokenManager ?? throw new ArgumentNullException(nameof(jwtTokenManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

        }

        public async Task<string> Login(LoginRequestDTO dto)
        {
            var auth = await _authRepository.FindByEmailAsync(dto.Email);

            if (auth == null)
            {
                throw new GeneralException(ErrorType.EMAIL_OR_PASSWORD_WRONG);
            }

            bool passwordMatches = _passwordEncoder.Matches(dto.Password, auth.Password);

            if (!passwordMatches)
            {
                throw new GeneralException(ErrorType.PASSWORD_WRONG);
            }

            string token = await _jwtTokenManager.GenerateToken(auth.Id);

            if (string.IsNullOrEmpty(token))
            {
                throw new GeneralException(ErrorType.TOKEN_CREATION_FAILED);
            }

            return token;
        }


        public async Task<bool> ResetPassword(ResetPasswordRequestDTO dto)
        {
            var auth = await _authRepository.FindByCodeAsync(dto.Code);

            if (auth == null)
            {
                throw new GeneralException(ErrorType.USER_NOT_FOUND);
            }

            if (!dto.NewPassword.Equals(dto.RePassword))
            {
                throw new GeneralException(ErrorType.PASSWORD_MISMATCH);
            }


            // Reset kodunun süresini kontrol ediyoruz
            CodeGenerator.ResetCode resetCode = new CodeGenerator.ResetCode(auth.Code, auth.CodeTimestamp);
            if (resetCode.IsExpired())
            {
                throw new GeneralException(ErrorType.EXPIRED_RESET_CODE);
            }
            // Yeni şifreyi şifreleyip kaydediyoruz

            var encodedPassword = _passwordEncoder.Encode(dto.NewPassword);
            auth.Password = encodedPassword;

            await _authRepository.SaveAsync(auth);

            return true;
        }

        public async Task<Auth> GetAuthFromToken(string token)
        {
            long authId = await ExtractAuthIdFromToken(token);

            var auth = await _authRepository.FindByIdAsync(authId);

            if (auth == null)
            {
                throw new GeneralException(ErrorType.AUTH_NOT_FOUND);
            }

            return auth;
        }

        public async Task<long> ExtractAuthIdFromToken(string token)
        {
            var authIdOptional = _jwtTokenManager.GetAuthIdFromToken(token);

            if (authIdOptional.HasValue)
            {
                return await Task.FromResult(authIdOptional.Value);
            }
            else
            {
                throw new GeneralException(ErrorType.INVALID_TOKEN);
            }
        }

        public async Task<string> ForgetPasswordAsync(string email)
        {
            try
            {
                // Şifre sıfırlama kodunu üret
                var resetCode = CodeGenerator.GenerateResetPasswordCode();
                if (resetCode == null || string.IsNullOrEmpty(resetCode.Code))
                {
                    throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR);
                }
                Console.WriteLine("Generated Reset Code: " + resetCode.Code);

                // MailModel oluştur
                var mailModel = new MailModel
                {
                    Email = email,
                    Code = resetCode.Code
                };

                // Kullanıcıyı veritabanından bul
                var auth = await _authRepository.FindByEmailAsync(email);
                if (auth == null)
                {
                    throw new GeneralException(ErrorType.AUTH_NOT_FOUND);
                }

                // Kod süresi dolmuşsa, kodu temizle ve hata fırlat
                if (resetCode.IsExpired())
                {
                    auth.Code = null;
                    auth.CodeTimestamp = 0;

                    await _authRepository.SaveAsync(auth); // Veritabanında güncelleme yap
                    throw new GeneralException(ErrorType.EXPIRED_RESET_CODE);
                }

                // Kullanıcıyı güncelle
                auth.Code = mailModel.Code;
                auth.CodeTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                try
                {
                    // Veritabanını kaydet
                    await _authRepository.SaveAsync(auth);
                    Console.WriteLine("Auth record updated successfully with reset code.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Database error: " + e);
                    throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR);
                }

                // E-posta gönderimi
                try
                {
                    await _emailService.SendMailAsync(mailModel); // Asenkron mail gönderimi
                    Console.WriteLine("Reset code email sent to: " + email);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Email sending error: " + e);
                    throw new GeneralException(ErrorType.EMAIL_SEND_FAILED);
                }

                // Şifre sıfırlama kodu gönderildi mesajı
                return $"Şifre yenileme kodunuz {email} adresine gönderildi.";
            }
            catch (GeneralException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex}");
                throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
