using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Event.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Cart.DataProvider.Repositories
{
    public class CartRepository : ICartRepository
    {
        public Task<CartItemCreated> AddItem(AddCartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task<GetCartResult> GetCart(GetCart getCart)
        {
            throw new NotImplementedException();
        }

        public Task<CartRemoved> RemoveCart(RemoveCart removeCart)
        {
            throw new NotImplementedException();
        }

        public Task<CartItemRemoved> RemoveItem(RemoveCartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
