using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Event.Product;
using Eshop.Infrastructure.Query.Product;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Bulkhead;
using Polly.CircuitBreaker;
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
        private IBusControl busControl;
        private IScopedClientFactory ClientFactory;
        private static readonly int MaxRetryCount = 1;

        #region Policy List

        private AsyncFallbackPolicy<IActionResult> fallbackPolicy;
        private static AsyncCircuitBreakerPolicy<IActionResult> circuitBreakerPolicy = Policy<IActionResult>.Handle<Exception>().AdvancedCircuitBreakerAsync(0.5, TimeSpan.FromSeconds(30), 2, TimeSpan.FromMinutes(1));
        private static AsyncRetryPolicy<IActionResult> retryPolicy = Policy<IActionResult>.Handle<Exception>().WaitAndRetryAsync(MaxRetryCount, retryCount => TimeSpan.FromSeconds(Math.Pow(3, retryCount) / 3));
        private static AsyncPolicyWrap<IActionResult> policyWrap = Policy.WrapAsync(circuitBreakerPolicy, retryPolicy);
        private static AsyncBulkheadPolicy bulkheadPolicy = Policy.BulkheadAsync(1, 2, (ctx) => {
            throw new Exception("All slot are filled");
        });
        #endregion

        public ProductController(IBusControl bus, IScopedClientFactory clientFactory)
        {
            busControl = bus;
            ClientFactory = clientFactory;

            fallbackPolicy = Policy<IActionResult>.Handle<Exception>().FallbackAsync(Content("Error in call"));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string id)
        {
            //var circuitState = circuitBreakerPolicy.CircuitState;
            //var AvailableCount = bulkheadPolicy.BulkheadAvailableCount;
            //var EmptyCount = bulkheadPolicy.QueueAvailableCount;

            return await bulkheadPolicy.ExecuteAsync(async () =>
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
