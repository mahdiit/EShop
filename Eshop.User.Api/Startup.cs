using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.EventBus;
using Eshop.Infrastructure.Mongo;
using Eshop.Infrastructure.Security;
using Eshop.User.Api.Handlers;
using Eshop.User.Api.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.User.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddMongoDb(Configuration);
            services.AddSingleton<IDbInit, DbInit>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddScoped<CreateUserHandler>();
            services.AddScoped<UserValidationHandler>();

            var rabbitMqConfig = new RabbitMqConfig();
            Configuration.Bind("rabbitmq", rabbitMqConfig);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateUserHandler>();
                x.AddConsumer<UserValidationHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqConfig.ConnectionString), hostConfig =>
                    {
                        hostConfig.Username(rabbitMqConfig.Username);
                        hostConfig.Password(rabbitMqConfig.Password);
                    });

                    config.ReceiveEndpoint("create-user", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(cfg => { cfg.Interval(2, 100); });
                        ep.ConfigureConsumer<CreateUserHandler>(provider);
                    });

                    config.ReceiveEndpoint("valid-user", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(cfg => { cfg.Interval(2, 100); });
                        ep.ConfigureConsumer<UserValidationHandler>(provider);
                    });

                }));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();            
            app.UseAuthorization();
            app.ApplicationServices.GetService<IDbInit>().InitAsync();
            app.ApplicationServices.GetService<IBusControl>().Start();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
