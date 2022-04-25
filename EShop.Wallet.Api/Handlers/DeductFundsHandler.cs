using Eshop.Infrastructure.Command.Wallet;
using MassTransit;
using System.Threading.Tasks;

namespace EShop.Wallet.Api.Handlers
{
    public class DeductFundsHandler : IConsumer<DeductFunds>
    {
        public async Task Consume(ConsumeContext<DeductFunds> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
