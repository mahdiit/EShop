using Eshop.Infrastructure.Athuntication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Extention
{
    public static class Extention
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAthunticationHandler, AthunticationHandler>();
            var JwtConfig = new JwtConfig();
            configuration.Bind("jwt", JwtConfig);

            var SignSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecrectKey));

            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidIssuer = JwtConfig.Issuer,
                    IssuerSigningKey = SignSecurityKey
                };
            });
            return services;
        }
    }
}
