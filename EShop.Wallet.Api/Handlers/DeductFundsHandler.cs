using Eshop.Infrastructure.Command.Wallet;
using EShop.Wallet.Api.Services;
using MassTransit;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Handlers
{
    public class DeductFundsHandler : IConsumer<DeductFunds>
    {
        IWalletService Service;

        public DeductFundsHandler(IWalletService walletService)
        {
            Service = walletService;
        }

        public async Task Consume(ConsumeContext<DeductFunds> context)
        {
            var result = await Service.DeductFunds(context.Message);
            await context.RespondAsync(result);
        }
    }
}
