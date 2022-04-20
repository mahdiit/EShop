using Eshop.Infrastructure.Command.Order;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Handlers
{
    public class CreateOrderHandler : IConsumer<CreateOrder>
    {
        public Task Consume(ConsumeContext<CreateOrder> context)
        {
            throw new NotImplementedException();
        }
    }
}
