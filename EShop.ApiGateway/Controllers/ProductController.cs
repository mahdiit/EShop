using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Event.Product;
using Eshop.Infrastructure.Query.Product;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Fallback;
using Polly.Retry;
using Polly.Wrap;
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
        readonly AsyncFallbackPolicy<IActionResult> fallbackPolicy;
        readonly AsyncRetryPolicy<IActionResult> retryPolicy;
        readonly int MaxRetryCount = 4;

        readonly AsyncPolicyWrap<IActionResult> policyWrap;

        public ProductController(IBusControl bus, IScopedClientFactory clientFactory)
        {
            busControl = bus;
            ClientFactory = clientFactory;
            fallbackPolicy = Policy<IActionResult>
                .Handle<Exception>()
                .FallbackAsync(Content("Error in call"));

            retryPolicy = Policy<IActionResult>.Handle<Exception>().WaitAndRetryAsync(MaxRetryCount,
                retryCount => TimeSpan.FromSeconds(Math.Pow(3, retryCount) / 3));

            policyWrap = Policy.WrapAsync(fallbackPolicy, retryPolicy);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string id)
        {
            return await policyWrap.ExecuteAsync(async () =>
            {
                var request = ClientFactory.CreateRequestClient<GetProductById>();
                var response = await request.GetResponse<ProductCreated>(new GetProductById() { Id = id });
                return Accepted(response.Message);
            });
        }


        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Add([FromForm] CreateProduct product)
        {
            if (User != null)
            {
                var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                product.CreatedUserId = userId;
                product.CreatedAt = DateTime.Now;
            }
            
            var uri = new Uri("queue:create-product");
            var endPoint = await busControl.GetSendEndpoint(uri);
            await endPoint.Send(product);

            return Accepted("Product Created");
        }
    }
}
