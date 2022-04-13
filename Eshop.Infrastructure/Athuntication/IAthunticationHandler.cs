using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Eshop.Infrastructure.Athuntication
{
    public interface IAthunticationHandler
    {
        JwtAuthToken Create(Guid userId);
    }

    public class AthunticationHandler : IAthunticationHandler
    {
        private readonly JwtSecurityTokenHandler tokenHandler;
        JwtConfig JwtConfig;
        SecurityKey SignSecurityKey;
        SigningCredentials Credentials;
        JwtHeader JwtHeaders;
        TokenValidationParameters ValidationParameters;

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
        public JwtAuthToken Create(Guid userId)
        {
            
        }
    }
}
