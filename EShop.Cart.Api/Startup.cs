using Eshop.Infrastructure.EventBus;
using EShop.Cart.Api.Handlers;
using EShop.Cart.DataProvider.Repositories;
using EShop.Cart.DataProvider.Services;
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

namespace EShop.Cart.Api
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
            services.AddStackExchangeRedisCache(setup =>
            {
                setup.Configuration = $"{Configuration["redis:Host"]}:{Configuration["redis:Port"]}";
            });
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<AddCartItemHandler>();
            services.AddScoped<GetCartHandler>();
            services.AddScoped<RemoveCartHandler>();
            services.AddScoped<RemoveCartItemHandler>();

            var rabbitMqConfig = new RabbitMqConfig();
            Configuration.Bind("rabbitmq", rabbitMqConfig);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AddCartItemHandler>();
                x.AddConsumer<GetCartHandler>();
                x.AddConsumer<RemoveCartHandler>();
                x.AddConsumer<RemoveCartItemHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqConfig.ConnectionString), hostConfig =>
                    {
                        hostConfig.Username(rabbitMqConfig.Username);
                        hostConfig.Password(rabbitMqConfig.Password);
                    });

                    config.ReceiveEndpoint("add-cartItem", ep => { ep.ConfigureConsumer<AddCartItemHandler>(provider); });
                    config.ReceiveEndpoint("get-cart", ep => { ep.ConfigureConsumer<GetCartHandler>(provider); });
                    config.ReceiveEndpoint("remove-cartItem", ep => { ep.ConfigureConsumer<RemoveCartHandler>(provider); });
                    config.ReceiveEndpoint("remove-cart", ep => { ep.ConfigureConsumer<RemoveCartItemHandler>(provider); });
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

            app.ApplicationServices.GetService<IBusControl>().Start();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
