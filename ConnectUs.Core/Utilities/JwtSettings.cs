namespace ConnectUs.Core.Utilities
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } // appsettings.json'daki SecretKey
        public string Issuer { get; set; }    // appsettings.json'daki Issuer
    }
}
