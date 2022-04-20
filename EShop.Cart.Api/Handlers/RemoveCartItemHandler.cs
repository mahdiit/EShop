using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Command.Cart;
using EShop.Cart.DataProvider.Services;
using MassTransit;
using System.Threading.Tasks;

namespace EShop.Cart.Api.Handlers
{
    public class RemoveCartItemHandler : IConsumer<RemoveCartItem>
    {
        ICartService Service;
        public RemoveCartItemHandler(ICartService cartService)
        {
            Service = cartService;
        }
        public async Task Consume(ConsumeContext<RemoveCartItem> context)
        {
            var result = await Service.RemoveItem(context.Message);
            await context.RespondAsync<CartItemRemoved>(result);
        }
    }
}
