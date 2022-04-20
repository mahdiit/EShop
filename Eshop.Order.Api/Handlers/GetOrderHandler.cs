using Eshop.Infrastructure.Command.Order;
using Eshop.Order.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Handlers
{
    public class GetOrderHandler : IConsumer<GetOrder>
    {
        IOrderService orderService;
        public GetOrderHandler(IOrderService service)
        {
            orderService = service;
        }
        public async Task Consume(ConsumeContext<GetOrder> context)
        {
            var result = await orderService.GetOrder(context.Message);
            await context.RespondAsync(result);
        }
    }
}
