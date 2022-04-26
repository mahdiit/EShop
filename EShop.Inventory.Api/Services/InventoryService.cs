using Eshop.Infrastructure.Command.Inventory;
using Eshop.Infrastructure.Event.Inventory;
using EShop.Inventory.Api.Repositories;
using System.Threading.Tasks;

namespace EShop.Inventory.Api.Services
{
    public class InventoryService : IInventoryService
    {
        IInventoryRepository Repository;

        public InventoryService(IInventoryRepository repository)
        {
            Repository = repository;
        }

        public async Task<ProductAllocated> AllocateProduct(AllocateProduct product)
        {
            return await Repository.AllocateProduct(product);
        }

        public async Task<ProductReleased> ReleaseProduct(ReleaseProduct product)
        {
            return await Repository.ReleaseProduct(product);
        }
    }
}
