using Eshop.Infrastructure.Command.Wallet;
using EShop.Wallet.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Handlers
{
    public class AddFundsHandler : IConsumer<AddFunds>
    {
        IWalletService Service;
        public AddFundsHandler(IWalletService walletService)
        {
            Service = walletService;
        }
        public async Task Consume(ConsumeContext<AddFunds> context)
        {
            var result = await Service.AddFunds(context.Message);
            await context.RespondAsync(result);
        }
    }
}
