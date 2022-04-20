using Eshop.Infrastructure.Command.Order;
using Eshop.Infrastructure.Event.Order;
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
    public class OrderController : ControllerBase
    {
        IScopedClientFactory ClientFactory;
        public OrderController(IScopedClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder([FromQuery]GetOrder getOrder)
        {
            var request = ClientFactory.CreateRequestClient<GetOrder>();
            var response = await request.GetResponse<GetOrderResult>(getOrder);

            return Ok(response.Message);
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder([FromQuery] GetAllOrder getOrder)
        {
            var request = ClientFactory.CreateRequestClient<GetAllOrder>();
            var response = await request.GetResponse<GetAllOrderResult>(getOrder);

            return Ok(response.Message);
        }
    }
}
