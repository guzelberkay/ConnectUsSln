namespace ConnectUs.Core.Utilities
{
        public class CodeGenerator
        {
        private static readonly long EXPIRATION_TIME = (long)TimeSpan.FromSeconds(300).TotalMilliseconds;


        // Kod ve zaman damgası tutan sınıf
        public class ResetCode
            {
                public string Code { get; }
                public long Timestamp { get; }

                public ResetCode(string code, long timestamp)
                {
                    Code = code;
                    Timestamp = timestamp;
                }

                // Kodun süresi dolmuş mu kontrol et
                public bool IsExpired()
                {
                    return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Timestamp > EXPIRATION_TIME;
                }
            }

            // Şifre sıfırlama kodu üretme
            public static ResetCode GenerateResetPasswordCode()
            {
                string code = GenerateCode();  // Kod üret
                long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();  // Kod üretildiği zaman

                // Üretilen kodu ve zaman damgasını logla
                Console.WriteLine($"Generated reset password code: {code} at timestamp: {timestamp}");

                return new ResetCode(code, timestamp);  // Kod ve zaman damgası döndür
            }

            // Genel kod üretme fonksiyonu
            private static string GenerateCode()
            {
                string uuid = Guid.NewGuid().ToString();
                return uuid.Split('-')[0];  // UUID'nin ilk bölümünü alıyoruz
            }
        }
    }
    
