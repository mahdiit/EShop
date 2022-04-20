using Eshop.Infrastructure.Athuntication;
using Eshop.Infrastructure.Event.User;
using Eshop.Infrastructure.Query.User;
using Eshop.Infrastructure.Security;
using Eshop.User.Api.Repositories;
using Eshop.User.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.User.Api.Handlers
{
    public class UserValidationHandler : IConsumer<ValidateUser>
    {
        IUserService UserService;
        IEncrypter Encrypter;
        IAthunticationHandler Athuntication;

        public UserValidationHandler(IUserService userService, IEncrypter encrypter, IAthunticationHandler handler)
        {
            UserService = userService;
            Encrypter = encrypter;
            Athuntication = handler;
        }
        public async Task Consume(ConsumeContext<ValidateUser> context)
        {
            var user = await UserService.GetUser(context.Message.Username);
            if (user == null)
                await context.RespondAsync<UserValidate>(new UserValidate()
                {
                    Error = new List<string>() { "user invalid: username" }
                });

            if (user.Password.Equals(Encrypter.GetHash(context.Message.Password, user.Salt)))
            {
                await context.RespondAsync<UserValidate>(new UserValidate()
                {
                    Error= new List<string>() { "valid" },
                    LoginToken = Athuntication.Create(user.UserId),
                    IsValid = true
                });
            }
            else
                await context.RespondAsync<UserValidate>(new UserValidate()
                {
                    Error = new List<string>() { "user invalid: password" }
                });
        }
    }
}
