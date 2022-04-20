using Eshop.Infrastructure.Command.User;
using Eshop.Infrastructure.Event.User;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.User.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        IMongoDatabase Db;
        IMongoCollection<CreateUser> Table;

        public UserRepository(IMongoDatabase db)
        {
            Db = db;
            Table = db.GetCollection<CreateUser>("user");
        }

        public async Task<UserCreated> AddUser(CreateUser user)
        {
            await Table.InsertOneAsync(user);
            return new UserCreated()
            {
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                UserId = user.UserId,
                Username = user.Username
            };
        }

        public Task<UserCreated> GetUser(string userName)
        {
            var user = Table.AsQueryable().Where(x => x.Username == userName).FirstOrDefault();
            if (user == null)
                return null;

            return Task.FromResult(new UserCreated()
            {
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                Salt = user.Salt
            });
        }
    }
}
