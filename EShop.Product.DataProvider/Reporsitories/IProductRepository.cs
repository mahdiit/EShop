using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Command.Product;
using System.Threading.Tasks;

namespace EShop.Product.DataProvider.Reporsitories
{
    public interface IProductRepository
    {
        Task<ProductCreated> GetProduct(string id);
        Task<ProductCreated> AddProduct(CreateProduct createProduct);
    }
}
