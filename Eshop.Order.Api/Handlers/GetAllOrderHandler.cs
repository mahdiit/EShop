using Eshop.Infrastructure.Command.Order;
using Eshop.Order.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Handlers
{
    public class GetAllOrderHandler : IConsumer<GetAllOrder>
    {
        IOrderService orderService;
        public GetAllOrderHandler(IOrderService order)
        {
            orderService = order;
        }
        public async Task Consume(ConsumeContext<GetAllOrder> context)
        {
            var result = await orderService.GetAllOrder(context.Message);
            await context.RespondAsync(result);
        }
    }
}
