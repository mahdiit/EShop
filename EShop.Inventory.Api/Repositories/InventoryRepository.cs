using Eshop.Infrastructure.Command.Inventory;
using Eshop.Infrastructure.Event.Inventory;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Inventory.Api.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        IMongoDatabase Database;
        IMongoCollection<StockItem> Table;
        public InventoryRepository(IMongoDatabase database)
        {
            Database = database;
            Table = database.GetCollection<StockItem>("stock");
        }

        public async Task<ProductAllocated> AllocateProduct(AllocateProduct product)
        {
            List<WriteModel<StockItem>> productUpdateModel = new List<WriteModel<StockItem>>();
            foreach (var item in product.Items)
            {
                var currentStock = await Table.AsQueryable().FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                if(currentStock == null)
                {
                    currentStock = new StockItem() { ProductId = item.ProductId, Quantity = 0 };
                }
                currentStock.Quantity += item.Quantity;

                var filter = Builders<StockItem>.Filter.Eq("productId", currentStock.ProductId);
                var update = Builders<StockItem>.Update.Set("quantity", currentStock.Quantity);

                productUpdateModel.Add(new UpdateOneModel<StockItem>(filter, update) { IsUpsert = true });
            }

            await Table.BulkWriteAsync(productUpdateModel);
            return new ProductAllocated() { IsSucess = true };
        }

        public async Task<ProductReleased> ReleaseProduct(ReleaseProduct product)
        {
            List<WriteModel<StockItem>> productUpdateModel = new List<WriteModel<StockItem>>();
            foreach (var item in product.Items)
            {
                var currentStock = await Table.AsQueryable().FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                if (currentStock == null)
                    continue;

                currentStock.Quantity -= item.Quantity;
                var filter = Builders<StockItem>.Filter.Eq("productId", currentStock.ProductId);
                var update = Builders<StockItem>.Update.Set("quantity", currentStock.Quantity);

                productUpdateModel.Add(new UpdateOneModel<StockItem>(filter, update) { IsUpsert = true });
            }

            await Table.BulkWriteAsync(productUpdateModel);
            return new ProductReleased() { IsSucess = true };
        }
    }
}
