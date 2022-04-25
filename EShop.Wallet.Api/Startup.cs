using Eshop.Infrastructure.EventBus;
using Eshop.Infrastructure.Mongo;
using EShop.Wallet.Api.Handlers;
using EShop.Wallet.Api.Repositories;
using EShop.Wallet.Api.Services;
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

namespace EShop.Wallet.Api
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

            services.AddScoped<AddFundsHandler>();
            services.AddScoped<DeductFundsHandler>();

            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletService, WalletService>();

            var rabbitMqConfig = new RabbitMqConfig();
            Configuration.Bind("rabbitmq", rabbitMqConfig);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AddFundsHandler>();
                x.AddConsumer<DeductFundsHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqConfig.ConnectionString), hostConfig =>
                    {
                        hostConfig.Username(rabbitMqConfig.Username);
                        hostConfig.Password(rabbitMqConfig.Password);
                    });

                    config.ReceiveEndpoint("add-funds", ep => { ep.ConfigureConsumer<AddFundsHandler>(provider); });
                    config.ReceiveEndpoint("deduct-funds", ep => { ep.ConfigureConsumer<DeductFundsHandler>(provider); });                    
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
