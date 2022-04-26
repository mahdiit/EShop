using Eshop.Infrastructure.Command.Inventory;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Inventory.Api.Handlers
{
    public class ReleaseProductHandler : IConsumer<ReleaseProduct>
    {
        public ReleaseProductHandler()
        {

        }

        public Task Consume(ConsumeContext<ReleaseProduct> context)
        {
            throw new NotImplementedException();
        }
    }
}
