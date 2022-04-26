using Eshop.Infrastructure.Command.Inventory;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Inventory.Api.Handlers
{
    public class AllocateProductHandler : IConsumer<AllocateProduct>
    {
        public AllocateProductHandler()
        {

        }
        public Task Consume(ConsumeContext<AllocateProduct> context)
        {
            throw new NotImplementedException();
        }
    }
}
