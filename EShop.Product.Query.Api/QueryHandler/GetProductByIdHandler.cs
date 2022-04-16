using Eshop.Infrastructure.Event.Product;
using Eshop.Infrastructure.Query.Product;
using EShop.Product.DataProvider.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Query.Api.QueryHandler
{
    public class GetProductByIdHandler : IConsumer<GetProductById>
    {
        IProductService ServiceApi;

        public GetProductByIdHandler(IProductService service)
        {
            ServiceApi = service;
        }
        public async Task Consume(ConsumeContext<GetProductById> context)
        {
            throw new Exception("Invalid");

            var prod = await ServiceApi.GetProduct(context.Message.Id);
            await context.RespondAsync<ProductCreated>(prod);
        }
    }
}
