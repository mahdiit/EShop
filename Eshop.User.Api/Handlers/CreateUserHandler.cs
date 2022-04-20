using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Security;
using Eshop.User.Api.Repositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.User.Api.Handlers
{
    public class CreateUserHandler : IConsumer<CreateUser>
    {
        IUserRepository UserRepository;
        IEncrypter Encrypter;
        public CreateUserHandler(IUserRepository userRepository, IEncrypter encrypter)
        {
            UserRepository = userRepository;
            Encrypter = encrypter;
        }

        public async Task Consume(ConsumeContext<CreateUser> context)
        {
            var salt = Encrypter.GetSalt();
            context.Message.Salt = salt;
            context.Message.Password = Encrypter.GetHash(context.Message.Password, salt);
            var user = await UserRepository.AddUser(context.Message);
            await context.RespondAsync<UserCreated>(user);
        }
    }
}
