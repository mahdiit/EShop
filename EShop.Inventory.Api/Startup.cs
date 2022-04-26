using Eshop.Infrastructure.EventBus;
using Eshop.Infrastructure.Mongo;
using EShop.Inventory.Api.Handlers;
using EShop.Inventory.Api.Repositories;
using EShop.Inventory.Api.Services;
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

namespace EShop.Inventory.Api
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

            services.AddScoped<AllocateProductHandler>();
            services.AddScoped<ReleaseProductHandler>();

            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryService, InventoryService>();

            var rabbitMqConfig = new RabbitMqConfig();
            Configuration.Bind("rabbitmq", rabbitMqConfig);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AllocateProductHandler>();
                x.AddConsumer<ReleaseProductHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqConfig.ConnectionString), hostConfig =>
                    {
                        hostConfig.Username(rabbitMqConfig.Username);
                        hostConfig.Password(rabbitMqConfig.Password);
                    });

                    config.ReceiveEndpoint("allocate-product", ep => { ep.ConfigureConsumer<AllocateProductHandler>(provider); });
                    config.ReceiveEndpoint("release-product", ep => { ep.ConfigureConsumer<ReleaseProductHandler>(provider); });
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
