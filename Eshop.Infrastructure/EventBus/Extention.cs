using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Command.Inventory;
using Eshop.Infrastructure.Command.Order;
using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Command.Wallet;
using Eshop.Infrastructure.Query.Product;
using Eshop.Infrastructure.Query.User;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.EventBus
{
    public static class Extention
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration config)
        {
            var rabbitMqConfig = new RabbitMqConfig();
            config.Bind("rabbitmq", rabbitMqConfig);

            services.AddMassTransit(x=> {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqConfig.ConnectionString), hostConfig =>
                    {
                        hostConfig.Username(rabbitMqConfig.Username);
                        hostConfig.Password(rabbitMqConfig.Password);
                    });
                }));

                x.AddRequestClient<GetProductById>(new Uri("exchange:get-product"));
                x.AddRequestClient<CreateUser>(new Uri("exchange:create-user"));
                x.AddRequestClient<ValidateUser>(new Uri("exchange:valid-user"));

                x.AddRequestClient<GetCart>(new Uri("exchange:get-cart"));
                x.AddRequestClient<RemoveCart>(new Uri("exchange:remove-cart"));
                x.AddRequestClient<AddCartItem>(new Uri("exchange:add-cartItem"));
                x.AddRequestClient<RemoveCartItem>(new Uri("exchange:remove-cartItem"));

                x.AddRequestClient<CreateOrder>(new Uri("exchange:create-order"));
                x.AddRequestClient<GetOrder>(new Uri("exchange:get-order"));
                x.AddRequestClient<GetAllOrder>(new Uri("exchange:getall-order"));

                x.AddRequestClient<AddFunds>(new Uri("exchange:add-funds"));
                x.AddRequestClient<DeductFunds>(new Uri("exchange:deduct-funds"));

                x.AddRequestClient<AllocateProduct>(new Uri("exchange:allocate-product"));
                x.AddRequestClient<ReleaseProduct>(new Uri("exchange:release-product"));
            });

            return services;
        }
    }
}
