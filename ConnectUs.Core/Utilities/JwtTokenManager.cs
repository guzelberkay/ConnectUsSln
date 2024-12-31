using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConnectUs.Core.Utilities
{
    public class JwtTokenManager : ITokenService
    {
     

        private readonly JwtSettings _jwtSettings;
        private readonly long _tokenExpirationInMilliseconds = 1000L * 60 * 60; // 1 hour

        public JwtTokenManager(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        // Token oluşturma   public Task<String> GenerateToken(GenerateTokenRequest request)
        public Task<string> GenerateToken(long authId)
        {
            // Hardcoded secret key, issuer, and audience
            string secretKey = "j2G79NLkltojnpr1dlY0hCUrKrDdH7tyEeBkz2VNNVB2IDDAUK";
            string validIssuer = "java14";
            

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("Secret key is not configured.");
            }

            // Create the symmetric security key
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var dateTimeNow = DateTime.UtcNow;

            // Create the JWT token
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: validIssuer,
                claims: new List<Claim> {
            new Claim("authId", authId.ToString()) // Ensure that authId is passed as string
                },
                notBefore: dateTimeNow,
                expires: dateTimeNow.Add(TimeSpan.FromMinutes(500)),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            // Return the generated token as a string
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
        }




        // Token doğrulama
        public ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = CreateTokenValidationParameters();

                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return null;
            }
        }

        // authId almak
        public long? GetAuthIdFromToken(string token)
        {
            var principal = ValidateToken(token);
            return principal?.FindFirst("authId")?.Value != null
                ? Convert.ToInt64(principal.FindFirst("authId").Value)
                : (long?)null;
        }

        // Email almak
        public string GetEmailFromToken(string token)
        {
            var principal = ValidateToken(token);
            return principal?.FindFirst("email")?.Value;
        }

       

        // Genel hata mesajları için bir yardımcı metot
        private Exception CreateTokenException() =>
            new Exception("Invalid token or token verification failed.");

        // İmzalama bilgileri oluşturma
        private SigningCredentials CreateSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }

        // Token doğrulama parametreleri oluşturma
        private TokenValidationParameters CreateTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey))
            };
        }
    }
}
