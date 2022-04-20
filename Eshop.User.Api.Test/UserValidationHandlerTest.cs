using Eshop.Infrastructure.Athuntication;
using Eshop.Infrastructure.Event.User;
using Eshop.Infrastructure.Query.User;
using Eshop.Infrastructure.Security;
using Eshop.User.Api.Handlers;
using Eshop.User.Api.Services;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.User.Api.Test
{
    public class UserValidationHandlerTest
    {
        [Test(TestOf = typeof(UserValidationHandler))]
        public async Task ValidateUser()
        {
            var user = new ValidateUser()
            {
                Username = "emami",
                Password = "123@Admin"
            };

            var userCreated = new UserCreated()
            {
                Username = "emami",
                Password = "123@Admin",
                ContactNumber = "1233",
                UserId = "emami"
            };

            var token = new JwtAuthToken() { Token = "ryxhsehx23345adf=-=", Expires = 110 };

            var harness = new InMemoryTestHarness();

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(userCreated);

            var authHandler = new Mock<IAthunticationHandler>();
            authHandler.Setup(x => x.Create(It.IsAny<string>())).Returns(token);

            var encrypt = new Mock<IEncrypter>();
            encrypt.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns(userCreated.Password);

            var consumerHarness = harness.Consumer(() => new UserValidationHandler(userService.Object,
                encrypt.Object, 
                authHandler.Object));

            await harness.Start();
            var requestClient = harness.CreateRequestClient<ValidateUser>();
            var userResult = await requestClient.GetResponse<UserValidate>(user);

            Assert.That(userResult.Message.LoginToken.Token == token.Token);
            await harness.Stop();
        }
    }
}