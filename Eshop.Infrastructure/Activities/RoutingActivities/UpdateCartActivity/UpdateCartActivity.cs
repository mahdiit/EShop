using Eshop.Infrastructure.Command.Cart;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Activities.RoutingActivities.UpdateCartActivity
{
    public class UpdateCartActivity : IExecuteActivity<GetCartResult>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<GetCartResult> context)
        {
            try
            {
                var endpoint = await context.GetSendEndpoint(new Uri("exchange:remove-cart"));
                await endpoint.Send(context.Arguments);

                return context.Completed();

            }
            catch (Exception)
            {
                //return context.Faulted();
                throw new Exception("Error logged");
            }
        }
    }
}
