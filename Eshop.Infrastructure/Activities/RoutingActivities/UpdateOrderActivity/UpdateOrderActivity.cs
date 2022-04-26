using Eshop.Infrastructure.Event.Order;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Activities.RoutingActivities.UpdateOrderActivity
{
    public class UpdateOrderActivity : IActivity<OrderCreated, UpdateOrderLog>
    {
        public Task<CompensationResult> Compensate(CompensateContext<UpdateOrderLog> context)
        {
            throw new NotImplementedException();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<OrderCreated> context)
        {
            try
            {
                var endpoint = await context.GetSendEndpoint(new Uri("exchange:create-order"));
                await endpoint.Send(context.Arguments);
                return context.CompletedWithVariables<UpdateOrderLog>(new UpdateOrderLog() { OrderId = context.Arguments.OrderId }, new { });
            }
            catch (Exception)
            {
                return context.Faulted();
            }
        }
    }
}
