using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Event.Cart;
using EShop.Cart.DataProvider.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Cart.DataProvider.Services
{
    public class CartService : ICartService
    {
        ICartRepository  Repository;
        public CartService(ICartRepository cartRepository)
        {
            Repository = cartRepository;
        }

        public async Task<CartItemCreated> AddItem(AddCartItem cartItem)
        {
            return await Repository.AddItem(cartItem);
        }

        public async Task<GetCartResult> GetCart(GetCart getCart)
        {
            return await Repository.GetCart(getCart);
        }

        public async Task<CartRemoved> RemoveCart(RemoveCart removeCart)
        {
            return await Repository.RemoveCart(removeCart);
        }

        public async Task<CartItemRemoved> RemoveItem(RemoveCartItem cartItem)
        {
            return await Repository.RemoveItem(cartItem);
        }
    }
}
