using Eshop.Infrastructure.Command.Product;
using EShop.Product.DataProvider.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Product.Api.Handlers
{
    public class CreateProductHandler : IConsumer<CreateProduct>
    {
        IProductService ServiceApi;
        public CreateProductHandler(IProductService service)
        {
            ServiceApi = service;
        }
        public async Task Consume(ConsumeContext<CreateProduct> context)
        {
            await ServiceApi.AddProduct(context.Message);
            await Task.CompletedTask;
        }
    }
}
