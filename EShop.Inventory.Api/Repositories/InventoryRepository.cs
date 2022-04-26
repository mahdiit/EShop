using Eshop.Infrastructure.Command.Inventory;
using Eshop.Infrastructure.Event.Inventory;
using System.Threading.Tasks;

namespace EShop.Inventory.Api.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        public Task<ProductAllocated> AllocateProduct(AllocateProduct product)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductReleased> ReleaseProduct(ReleaseProduct product)
        {
            throw new System.NotImplementedException();
        }
    }
}
