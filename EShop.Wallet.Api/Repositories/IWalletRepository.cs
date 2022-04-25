using Eshop.Infrastructure.Command.Wallet;
using Eshop.Infrastructure.Event.Wallet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Repositories
{
    public interface IWalletRepository
    {
        Task<FundsAdded> AddFunds(AddFunds addFunds);
        Task<FundsDeducted> DeductFunds(DeductFunds deductFunds);
    }
}
