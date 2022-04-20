using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Command.Cart;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShop.Cart.DataProvider.Repositories
{
    public class CartRepository : ICartRepository
    {
        IDistributedCache distributedCache;
        public CartRepository(IDistributedCache cache)
        {
            distributedCache = cache;
        }
        public async Task<CartItemCreated> AddItem(AddCartItem cartItem)
        {
            var cartData = await distributedCache.GetStringAsync(cartItem.UserId);
            GetCartResult cart;
            if (string.IsNullOrEmpty(cartData))
            {
                cart = new GetCartResult() { UserId = cartItem.UserId };
            }
            else
            {
                cart = JsonSerializer.Deserialize<GetCartResult>(cartData);
            }
            var item = new CartItemCreated() { Price = cartItem.Price, ProductId = cartItem.ProductId, Quanitty = cartItem.Amount };
            cart.Items.Add(item);

            await distributedCache.SetStringAsync(cartItem.UserId, JsonSerializer.Serialize(cart));
            return item;
        }

        public async Task<GetCartResult> GetCart(GetCart getCart)
        {
            var cartData = await distributedCache.GetStringAsync(getCart.UserId);
            GetCartResult cart;
            if (string.IsNullOrEmpty(cartData))
            {
                cart = new GetCartResult();
            }
            else
            {
                cart = JsonSerializer.Deserialize<GetCartResult>(cartData);
            }
            return cart;
        }

        public async Task<CartRemoved> RemoveCart(RemoveCart removeCart)
        {
            await distributedCache.SetStringAsync(removeCart.UserId, "");
            return new CartRemoved() { IsSucess = true };
        }

        public async Task<CartItemRemoved> RemoveItem(RemoveCartItem cartItem)
        {
            var cartData = await distributedCache.GetStringAsync(cartItem.UserId);
            GetCartResult cart;
            if (string.IsNullOrEmpty(cartData))
            {
                cart = new GetCartResult();
            }
            else
            {
                cart = JsonSerializer.Deserialize<GetCartResult>(cartData);
            }

            var item = cart.Items.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
            if(item != null)
            {
                cart.Items.Remove(item);
            }

            await distributedCache.SetStringAsync(cartItem.UserId, JsonSerializer.Serialize(cart));
            return new CartItemRemoved() { IsSucess = true };
        }
    }
}
