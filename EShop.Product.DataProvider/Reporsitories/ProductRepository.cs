using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Command.Product;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.DataProvider.Reporsitories
{
    public class ProductRepository : IProductRepository
    {
        IMongoDatabase Db;
        IMongoCollection<CreateProduct> Table;

        public ProductRepository(IMongoDatabase db)
        {
            Db = db;
            Table = db.GetCollection<CreateProduct>("product");
        }

        public async Task<ProductCreated> AddProduct(CreateProduct createProduct)
        {
            await Table.InsertOneAsync(createProduct);
            return new ProductCreated() { ProductId = createProduct.ProductId, ProductName = createProduct.ProductName, CreatedAt = DateTime.Now };
        }

        public async Task<ProductCreated> GetProduct(string id)
        {
            var createProduct = Table.AsQueryable().Where(x => x.ProductId == id).FirstOrDefault();
            if (createProduct == null)
                throw new Exception("Product is null");

            await Task.CompletedTask;
            return new ProductCreated() { ProductId = createProduct.ProductId, ProductName = createProduct.ProductName, CreatedAt = DateTime.Now };
        }
    }
}
