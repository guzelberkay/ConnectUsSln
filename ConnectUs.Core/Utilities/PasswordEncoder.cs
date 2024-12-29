using BCrypt.Net;

namespace ConnectUs.Core.Utilities
{
    public class PasswordEncoder
    {
        // Şifreyi hashlemek için kullanılan metod
        public string Encode(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); // BCrypt ile şifreyi hashler
        }

        // Şifrenin doğruluğunu kontrol etmek için kullanılan metod
        public bool Matches(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword); // BCrypt ile şifre doğrulaması yapar
        }
    }
}
