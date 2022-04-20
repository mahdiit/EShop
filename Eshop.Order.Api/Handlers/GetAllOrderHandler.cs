using Eshop.Infrastructure.Command.Order;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Handlers
{
    public class GetAllOrderHandler : IConsumer<GetAllOrder>
    {
        public GetAllOrderHandler()
        {

        }
        public Task Consume(ConsumeContext<GetAllOrder> context)
        {
            throw new NotImplementedException();
        }
    }
}
