using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Query.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(GetProductById getProductById)
        {
            await Task.CompletedTask;
            return Accepted("as");
        }
    }
}
