namespace ConnectUs.Core.Utilities
{
    public class CodeGenerator
    {
        private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(5);

        public class ResetCode
        {
            public string Code { get; }
            public DateTime Timestamp { get; }

            public ResetCode(string code, DateTime timestamp)
            {
                Code = code;
                Timestamp = timestamp;
            }

            public bool IsExpired()
            {
                return DateTime.UtcNow - Timestamp > ExpirationTime;
            }
        }

        public static ResetCode GenerateResetPasswordCode()
        {
            string code = GenerateCode();
            var timestamp = DateTime.UtcNow;

            Console.WriteLine($"Generated reset password code: {code} at timestamp: {timestamp}");
            return new ResetCode(code, timestamp);
        }

        private static string GenerateCode()
        {
            return Guid.NewGuid().ToString().Split('-')[0]; // UUID'nin ilk bölümünü alıyoruz
        }
    }
}
