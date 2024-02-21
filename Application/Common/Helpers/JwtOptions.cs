namespace AssignmentApp.Application.Common.Helpers
{
    public class JwtOptions
    {
        public int ExpiresMinutes { get; set; }
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
