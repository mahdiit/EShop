using Eshop.Infrastructure.Command.Inventory;
using Eshop.Infrastructure.Event.Inventory;
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
    public class InventoryController : ControllerBase
    {
        IScopedClientFactory ClientFactory;
        public InventoryController(IScopedClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        [HttpPost("Release")]
        public async Task<IActionResult> Release([FromBody] ReleaseProduct product)
        {
            var request = ClientFactory.CreateRequestClient<ReleaseProduct>();
            var response = await request.GetResponse<ProductReleased>(product);

            return Ok(response.Message);
        }

        [HttpPost("Allocate")]
        public async Task<IActionResult> Allocate([FromBody] AllocateProduct product)
        {
            var request = ClientFactory.CreateRequestClient<AllocateProduct>();
            var response = await request.GetResponse<ProductAllocated>(product);

            return Ok(response.Message);
        }
    }
}
