using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConnectUs.Core.Utilities
{
    public class JwtTokenManager
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly long _tokenExpirationInMilliseconds = 1000L * 60 * 60; // 1 hour

        public JwtTokenManager(string secretKey, string issuer)
        {
            _secretKey = secretKey;
            _issuer = issuer;
        }

        // Token oluşturma (authId ile)
        public string CreateToken(long authId)
        {
            try
            {
                var claims = new[] {
            new Claim("authId", authId.ToString())  // Kullanıcı kimliği claim olarak ekleniyor
        };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddMilliseconds(_tokenExpirationInMilliseconds),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token); // Token oluşturuluyor ve geri döndürülüyor
            }
            catch (Exception)
            {
                return null; // Hata oluşursa null döndürülür
            }
        }


        // Token doğrulama ve authId alma
        public long? ValidateToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey
                };

                // Token doğrulama
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (principal == null)
                    return null;

                // authId değerini almak
                var authIdClaim = principal.FindFirst("authId");
                return authIdClaim != null ? Convert.ToInt64(authIdClaim.Value) : (long?)null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Token'dan authId almak
        public long? GetAuthIdFromToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey
                };

                // Token doğrulama
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (principal == null)
                    return null;

                // authId değerini almak
                var authIdClaim = principal.FindFirst("authId");
                return authIdClaim != null ? Convert.ToInt64(authIdClaim.Value) : (long?)null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Token'dan ID almak
        public long? GetIdFromToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey
                };

                // Token doğrulama
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (principal == null)
                    throw new Exception("Invalid token.");

                // authId değerini almak
                var authIdClaim = principal.FindFirst("authId");
                if (authIdClaim == null)
                    throw new Exception("Authentication ID not found in token.");

                return Convert.ToInt64(authIdClaim.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Invalid token or token verification failed.");
            }
        }

        // Şifre sıfırlama token'ı oluşturma
        public string CreatePasswordResetToken(string email)
        {
            try
            {
                var claims = new[]
                {
                    new Claim("email", email)
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddMilliseconds(_tokenExpirationInMilliseconds),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Token'dan email almak
        public string GetEmailFromToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey
                };

                // Token doğrulama
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (principal == null)
                    throw new Exception("Invalid token.");

                // email değerini almak
                var emailClaim = principal.FindFirst("email");
                return emailClaim?.Value;
            }
            catch (Exception)
            {
                throw new Exception("Invalid token or token verification failed.");
            }
        }
    }
}
