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
    public class GetCartHandler : IConsumer<GetCart>
    {
        ICartService Service;
        public GetCartHandler(ICartService cartService)
        {
            Service = cartService;
        }
        public async Task Consume(ConsumeContext<GetCart> context)
        {
            var result = await Service.GetCart(context.Message);
            await context.RespondAsync<GetCartResult>(result);
        }
    }
}
