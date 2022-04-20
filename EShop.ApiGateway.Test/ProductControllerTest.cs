using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Query.Product;
using EShop.ApiGateway.Controllers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.ApiGateway.Test
{
    [TestFixture]
    public class ProductControllerTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Add()
        {
            var sendEndPoint = new Mock<ISendEndpoint>();
            var busControl = new Mock<IBusControl>();
            busControl.Setup(x => x.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(sendEndPoint.Object);
            var productController = new ProductController(busControl.Object, null);

            var result = await productController.Add(It.IsAny<CreateProduct>());

            Assert.IsTrue((result as AcceptedResult).StatusCode == (int?)HttpStatusCode.Accepted);
        }

        [Test]
        public async Task GetProduct()
        {
            var response = new Mock<Response<ProductCreated>>();
            var request = new Mock<IRequestClient<GetProductById>>();
            request.Setup(x => x
            .GetResponse<ProductCreated>(It.IsAny<GetProductById>(),
               It.IsAny<CancellationToken>(),
               It.IsAny<RequestTimeout>()))
                .ReturnsAsync(response.Object);

            var client = new Mock<IScopedClientFactory>();
            client.Setup(x => x.CreateRequestClient<GetProductById>(It.IsAny<RequestTimeout>())).Returns(request.Object);

            var productController = new ProductController(null, client.Object);

            var result = await productController.Get(It.IsAny<string>());

            Assert.IsTrue((result as AcceptedResult).StatusCode == (int?)HttpStatusCode.Accepted);
        }
    } 
}