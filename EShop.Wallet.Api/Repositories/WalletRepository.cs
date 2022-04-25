using Eshop.Infrastructure.Command.Wallet;
using Eshop.Infrastructure.Event.Wallet;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        IMongoDatabase MongoDatabase;
        IMongoCollection<WalletData> Table;

        public WalletRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
            Table = MongoDatabase.GetCollection<WalletData>("wallet");
        }

        public async Task<FundsAdded> AddFunds(AddFunds addFunds)
        {
            var userInfo =  await Table.AsQueryable().Where(x => x.UserId == addFunds.UserId).FirstOrDefaultAsync();
            if(userInfo == null)
            {
                await Table.InsertOneAsync(new WalletData() { UserId = addFunds.UserId, Amount = addFunds.CreditAmount });
                return new FundsAdded() { IsSucess = true };
            }
            else
            {
                userInfo.Amount += addFunds.CreditAmount;
                var filter = Builders<WalletData>.Filter.Eq("userId", userInfo.UserId);
                var update = Builders<WalletData>.Update.Set("amount", userInfo.Amount);
                await Table.UpdateOneAsync(filter, update);

                return new FundsAdded() { IsSucess = true };
            }
        }

        public async Task<FundsDeducted> DeductFunds(DeductFunds deductFunds)
        {
            var userInfo = await Table.AsQueryable().Where(x => x.UserId == deductFunds.UserId).FirstOrDefaultAsync();
            if (userInfo == null)
            {
                await Table.InsertOneAsync(new WalletData() { UserId = deductFunds.UserId, Amount = 0 - deductFunds.DebitAmount });
                return new FundsDeducted() { IsSucess = true };
            }
            else
            {
                userInfo.Amount -= deductFunds.DebitAmount;
                var filter = Builders<WalletData>.Filter.Eq("userId", userInfo.UserId);
                var update = Builders<WalletData>.Update.Set("amount", userInfo.Amount);
                await Table.UpdateOneAsync(filter, update);

                return new FundsDeducted() { IsSucess = true };
            }
        }
    }
}
