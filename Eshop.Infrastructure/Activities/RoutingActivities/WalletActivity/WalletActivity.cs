using Eshop.Infrastructure.Command.Wallet;
using Eshop.Infrastructure.Event.Wallet;
using MassTransit;
using MassTransit.Courier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Activities.RoutingActivities.WalletActivity
{
    public class WalletActivity : IActivity<TransMoney, TransMoneyLog>
    {
        public Task<CompensationResult> Compensate(CompensateContext<TransMoneyLog> context)
        {
            throw new NotImplementedException();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<TransMoney> context)
        {
            try
            {
                var deductFunds = new DeductFunds()
                {
                    DebitAmount = context.Arguments.Amount,
                    UserId = context.Arguments.UserId
                };

                var endpoint = await context.GetSendEndpoint(new Uri("exchange:deduct-funds"));
                await endpoint.Send(deductFunds);

                return context.CompletedWithVariables<TransMoneyLog>(new TransMoneyLog
                {

                    Amount = context.Arguments.Amount,
                    UserId = context.Arguments.UserId
                }, new { });
            }
            catch (Exception)
            {
                return context.Faulted();
            }
        }
    }
}
