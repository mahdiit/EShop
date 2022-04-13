using Eshop.Infrastructure.Command.Product;
using EShop.ApiGateway.Controllers;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
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
    } 
}