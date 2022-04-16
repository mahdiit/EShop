using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Event.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.User.Api.Services
{
    public interface IUserService
    {
        Task<UserCreated> AddUser(CreateUser user);
        Task<UserCreated> GetUser(string id);
    }
}
