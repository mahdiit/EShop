using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Event.User;
using Eshop.User.Api.Repositories;
using System.Threading.Tasks;

namespace Eshop.User.Api.Services
{
    public class UserService : IUserService
    {
        IUserRepository UserRepository;

        public UserService(IUserRepository repository)
        {
            UserRepository = repository;
        }

        public async Task<UserCreated> AddUser(CreateUser user)
        {
            return await UserRepository.AddUser(user);
        }

        public async Task<UserCreated> GetUser(string id)
        {
            return await UserRepository.GetUser(id);
        }
    }
}
