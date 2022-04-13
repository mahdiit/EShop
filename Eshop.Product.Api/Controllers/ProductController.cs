using Eshop.Infrastructure.Command.Product;
using EShop.Product.DataProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService ProductService { get; set; }

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await ProductService.GetProduct(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] CreateProduct createProduct)
        {            
            return Ok(await ProductService.AddProduct(createProduct));
                
        }
    }
}
