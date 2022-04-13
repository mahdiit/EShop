namespace Eshop.Infrastructure.Athuntication
{
    public class JwtAuthToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}