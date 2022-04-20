using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Command.Cart;
using EShop.Cart.DataProvider.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Cart.Api.Handlers
{
    public class RemoveCartHandler : IConsumer<RemoveCart>
    {
        ICartService Service;
        public RemoveCartHandler(ICartService cartService)
        {
            Service = cartService;
        }
        public async Task Consume(ConsumeContext<RemoveCart> context)
        {
            var result = await Service.RemoveCart(context.Message);
            await context.RespondAsync<CartRemoved>(result);
        }
    }
}
