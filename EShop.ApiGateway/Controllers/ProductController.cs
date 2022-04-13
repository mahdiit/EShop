using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Event.Product;
using Eshop.Infrastructure.Query.Product;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IBusControl busControl;        
        IScopedClientFactory ClientFactory;
        public ProductController(IBusControl bus, IScopedClientFactory clientFactory)
        {
            busControl = bus;
            ClientFactory = clientFactory;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await ClientFactory
                .CreateRequestClient<GetProductById>()
                .GetResponse<ProductCreated>(new GetProductById() { Id = id });            
            return Accepted(response.Message);
        }


        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Add([FromForm]CreateProduct product)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            product.CreatedUserId = userId;
            product.CreatedAt = DateTime.Now;

            var uri = new Uri("queue:create-product");
            var endPoint = await busControl.GetSendEndpoint(uri);
            await endPoint.Send(product);

            return Accepted("Product Created");
        }
    }
}
