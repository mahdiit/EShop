using Eshop.Infrastructure.Command.Wallet;
using Eshop.Infrastructure.Event.Wallet;
using EShop.Wallet.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Services
{
    public class WalletService : IWalletService
    {
        IWalletRepository Repository;
        public WalletService(IWalletRepository walletRepository)
        {
            Repository = walletRepository;
        }
        public async Task<FundsAdded> AddFunds(AddFunds addFunds)
        {
            return await Repository.AddFunds(addFunds);
        }

        public async Task<FundsDeducted> DeductFunds(DeductFunds deductFunds)
        {
            return await Repository.DeductFunds(deductFunds);
        }
    }
}
