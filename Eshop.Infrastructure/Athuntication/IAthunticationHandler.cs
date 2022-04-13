using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eshop.Infrastructure.Athuntication
{
    public interface IAthunticationHandler
    {
        JwtAuthToken Create(string userId);
        TokenValidationParameters ValidationParameters { get; set; }
    }

    public class AthunticationHandler : IAthunticationHandler
    {
        private readonly JwtSecurityTokenHandler tokenHandler;
        JwtConfig JwtConfig;
        SecurityKey SignSecurityKey;
        SigningCredentials Credentials;
        JwtHeader JwtHeaders;

        public TokenValidationParameters ValidationParameters { get; set; }

        public AthunticationHandler(IConfiguration configuration)
        {
            tokenHandler = new JwtSecurityTokenHandler();
            JwtConfig = new JwtConfig();
            configuration.Bind("jwt", JwtConfig);

            SignSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecrectKey));
            Credentials = new SigningCredentials(SignSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeaders = new JwtHeader(Credentials);
            ValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidIssuer = JwtConfig.Issuer,
                IssuerSigningKey = SignSecurityKey
            };
        }
        public JwtAuthToken Create(string userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(JwtConfig.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);

            var payload = new JwtPayload()
            {
                {"sub", userId },
                {"iss", JwtConfig.Issuer },
                {"iat", now },
                {"unique_name", userId },
                {"exp", exp  }
            };

            var jwt = new JwtSecurityToken(JwtHeaders, payload);
            var token = tokenHandler.WriteToken(jwt);
            var jsonToken = new JwtAuthToken() { Token = token, Expires = exp };


            return jsonToken;
        }
    }
}
