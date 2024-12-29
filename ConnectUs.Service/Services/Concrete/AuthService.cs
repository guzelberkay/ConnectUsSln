using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;
using static ConnectUs.Core.Utilities.CodeGenerator;

namespace ConnectUs.Service.Services.Concrete;


public class AuthService : IAuthService
{

    private readonly AuthRepository _authRepository;
    private readonly PasswordEncoder _passwordEncoder;
    private readonly JwtTokenManager _jwtTokenManager;
   
    public async Task<string> Login(LoginRequestDTO dto)
    {
        // E-posta ile Auth nesnesini veritabanından al
        var auth = await _authRepository.FindByEmailAsync(dto.Email); // Veritabanından e-posta ile kullanıcıyı alıyoruz

        // Kullanıcı bulunamadıysa veya şifre yanlışsa hata fırlat
        if (auth == null)
        {
            throw new GeneralException(ErrorType.EMAIL_OR_PASSWORD_WRONG); // Kullanıcı bulunamadı veya şifre yanlış
        }

        // Sağlanan şifreyi veritabanındaki şifreyle karşılaştır
        bool passwordMatches = _passwordEncoder.Matches(dto.Password, auth.Password); // Şifre kontrolü yapılıyor

        if (!passwordMatches)
        {
            throw new GeneralException(ErrorType.EMAIL_OR_PASSWORD_WRONG); // Şifre yanlış
        }

        // Kimlik doğrulanan kullanıcı için token oluştur
        string token = _jwtTokenManager.CreateToken(auth.Id); // Token oluşturma işlemi senkron olduğu için await gerekmez

        // Token oluşturulamadıysa hata fırlat
        if (string.IsNullOrEmpty(token))
        {
            throw new GeneralException(ErrorType.TOKEN_CREATION_FAILED); // Token oluşturulamadı
        }

        return token; // Token başarıyla oluşturulursa geri döndürülür
    }

    public async Task<bool> ResetPassword(ResetPasswordRequestDTO dto)
    {
        // Şifre sıfırlama kodunu kullanıcıyla eşleştiriyoruz
        var auth = await _authRepository.FindByCodeAsync(dto.Code);  // Asenkron işlemi bekliyoruz

        if (auth == null)
        {
            throw new GeneralException(ErrorType.USER_NOT_FOUND); // Kullanıcı bulunamadı
        }

        // Şifreler uyuşmuyorsa hata fırlatıyoruz
        if (!dto.NewPassword.Equals(dto.RePassword))
        {
            throw new GeneralException(ErrorType.PASSWORD_MISMATCH); // Şifreler uyuşmuyor
        }

        // Convert long (timestamp) to DateTime
        DateTime timestamp;
        try
        {
            timestamp = DateTimeOffset.FromUnixTimeSeconds(auth.CodeTimestamp).DateTime;  // long timestamp'ı DateTime'a çeviriyoruz
        }
        catch (Exception ex)
        {
            throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR); // Eğer dönüştürme işlemi başarısız olursa hata fırlatıyoruz
        }

        // Reset kodunun süresini kontrol ediyoruz
        var resetCode = new ResetCode(auth.Code, timestamp); // CodeTimestamp burada DateTime türünde
        if (resetCode.IsExpired())
        {
            throw new GeneralException(ErrorType.EXPIRED_RESET_CODE);  // Token süresi geçmişse hata ver
        }

        // Yeni şifreyi şifreleyip kaydediyoruz
        var encodedPassword = _passwordEncoder.Encode(dto.NewPassword); // Şifreyi şifrele
        auth.Password = encodedPassword;  // Şifreyi güncelle

        // Yeni şifreyi asenkron olarak kaydet
        await _authRepository.SaveAsync(auth);

        return true; // Şifre başarıyla sıfırlandı
    }


    public Task<bool> CheckEmailExists(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string> FindEmailByAuthId(long authId)
    {
        throw new NotImplementedException();
    }


    public Task<Auth> GetAuthFromToken(string token)
    {
        throw new NotImplementedException();
    }

    

    public Task<bool> LoginProfileManagement(string password, string token)
    {
        throw new NotImplementedException();
    }
   



}
