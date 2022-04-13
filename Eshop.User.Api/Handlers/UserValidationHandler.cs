﻿using Eshop.Infrastructure.Event.User;
using Eshop.Infrastructure.Query.User;
using Eshop.Infrastructure.Security;
using Eshop.User.Api.Repositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.User.Api.Handlers
{
    public class UserValidationHandler : IConsumer<ValidateUser>
    {
        IUserRepository UserRepository;
        IEncrypter Encrypter;

        public UserValidationHandler(IUserRepository userRepository, IEncrypter encrypter)
        {
            UserRepository = userRepository;
            Encrypter = encrypter;
        }
        public async Task Consume(ConsumeContext<ValidateUser> context)
        {
            var user = await UserRepository.GetUser(context.Message.Username);
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
