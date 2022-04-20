using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Query.User;
using Eshop.Infrastructure.Event.User;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IScopedClientFactory ClientFactory;
        public UserController(IScopedClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromForm]CreateUser user)
        {
            var result = await ClientFactory
                .CreateRequestClient<CreateUser>()
                .GetResponse<UserCreated>(user);

            return Ok(result.Message);
        }

        [HttpGet("ValidUser")]
        public async Task<IActionResult> ValidUser([FromForm] ValidateUser user)
        {
            var result = await ClientFactory
                .CreateRequestClient<ValidateUser>()
                .GetResponse<UserValidate>(user);

            return Ok(result.Message);
        }
    }
}
