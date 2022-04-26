using Eshop.Infrastructure.Command.Inventory;
using Eshop.Infrastructure.Event.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Inventory.Api.Services
{
    public interface IInventoryService
    {
        Task<ProductReleased> ReleaseProduct(ReleaseProduct product);
        Task<ProductAllocated> AllocateProduct(AllocateProduct product);
    }
}
