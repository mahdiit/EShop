using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Event.Cart;
using EShop.Cart.DataProvider.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Cart.Api.Handlers
{
    public class AddCartItemHandler : IConsumer<AddCartItem>
    {
        ICartService Service;
        public AddCartItemHandler(ICartService cartService)
        {
            Service = cartService;
        }
        public async Task Consume(ConsumeContext<AddCartItem> context)
        {
            var result = await Service.AddItem(context.Message);
            await context.RespondAsync<CartItemCreated>(result);
        }
    }
}
