using Eshop.Infrastructure.Command.Order;
using Eshop.Order.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Handlers
{
    public class CreateOrderHandler : IConsumer<CreateOrder>
    {
        IOrderService orderService;
        public CreateOrderHandler(IOrderService order)
        {
            orderService = order;
        }

        public Task Consume(ConsumeContext<CreateOrder> context)
        {
            var result = await orderService.CreateOrder(context.Message);
            await context.RespondAsync(result);
        }
    }
}
