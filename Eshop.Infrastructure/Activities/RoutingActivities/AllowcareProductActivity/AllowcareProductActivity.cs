using Eshop.Infrastructure.Command.Inventory;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Activities.RoutingActivities.AllowcareProductActivity
{
    public class AllowcareProductActivity : IActivity<AllocateProduct, OrderLog>
    {
        public Task<CompensationResult> Compensate(CompensateContext<OrderLog> context)
        {
            throw new NotImplementedException();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<AllocateProduct> context)
        {
            try
            {
                var endpoint = await context.GetSendEndpoint(new Uri("exchange:allocate-product"));
                var order = JsonSerializer.Deserialize<AllocateProduct>(context.Message.Variables["PlaceOrder"].ToString());

                await endpoint.Send(order);
                return context.CompletedWithVariables<AllocateProduct>(order, new { });
            }
            catch (Exception)
            {
                return context.Faulted();
            }            
        }
    }
}
