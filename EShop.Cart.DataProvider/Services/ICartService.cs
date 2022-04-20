using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Command.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Cart.DataProvider.Services
{
    public interface ICartService
    {
        public Task<GetCartResult> GetCart(GetCart getCart);
        public Task<CartRemoved> RemoveCart(RemoveCart removeCart);

        public Task<CartItemCreated> AddItem(AddCartItem cartItem);
        public Task<CartItemRemoved> RemoveItem(RemoveCartItem cartItem);
    }
}
