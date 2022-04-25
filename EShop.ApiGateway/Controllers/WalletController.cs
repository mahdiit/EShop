using Eshop.Infrastructure.Command.Wallet;
using Eshop.Infrastructure.Event.Wallet;
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
    public class WalletController : ControllerBase
    {
        IScopedClientFactory ClientFactory;
        public WalletController(IScopedClientFactory scopedClient)
        {
            ClientFactory = scopedClient;
        }

        [HttpPost("AddFunds")]
        public async Task<IActionResult> AddFunds([FromForm]AddFunds  fund)
        {
            var result = await ClientFactory
                .CreateRequestClient<AddFunds>()
                .GetResponse<FundsAdded>(fund);

            return Ok(result.Message);
        }

        [HttpPost("DeductFunds")]
        public async Task<IActionResult> DeductFunds([FromForm] DeductFunds fund)
        {
            var result = await ClientFactory
                .CreateRequestClient<DeductFunds>()
                .GetResponse<FundsDeducted>(fund);

            return Ok(result.Message);
        }
    }
}
