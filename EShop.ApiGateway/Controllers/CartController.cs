using Eshop.Infrastructure.Command.Cart;
using Eshop.Infrastructure.Event.Cart;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IScopedClientFactory ClientFactory;

        public CartController(IScopedClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart(GetCart getCart)
        {
            var request = ClientFactory.CreateRequestClient<GetCart>();
            var response = await request.GetResponse<GetCartResult>(getCart);

            return Ok(response.Message);
        }

        [HttpPost("RemoveCart")]
        public async Task<IActionResult> RemoveCart([FromForm]RemoveCart removeCart)
        {
            var request = ClientFactory.CreateRequestClient<RemoveCart>();
            var response = await request.GetResponse<CartRemoved>(removeCart);

            return Ok(response.Message);

        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromForm]AddCartItem cartItem)
        {
            var request = ClientFactory.CreateRequestClient<AddCartItem>();
            var response = await request.GetResponse<CartItemCreated>(cartItem);

            return Ok(response.Message);
        }

        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromForm]RemoveCartItem cartItem)
        {
            var request = ClientFactory.CreateRequestClient<RemoveCartItem>();
            var response = await request.GetResponse<CartItemRemoved>(cartItem);

            return Ok(response.Message);
        }
    }
}
