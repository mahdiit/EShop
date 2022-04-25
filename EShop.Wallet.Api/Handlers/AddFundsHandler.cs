using Eshop.Infrastructure.Command.Wallet;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Handlers
{
    public class AddFundsHandler : IConsumer<AddFunds>
    {
        public async Task Consume(ConsumeContext<AddFunds> context)
        {
            throw new NotImplementedException();
        }
    }
}
