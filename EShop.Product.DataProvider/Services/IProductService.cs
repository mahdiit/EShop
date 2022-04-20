using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Command.Product;
using System.Threading.Tasks;

namespace EShop.Product.DataProvider.Services
{
    public interface IProductService
    {
        Task<ProductCreated> GetProduct(string id);
        Task<ProductCreated> AddProduct(CreateProduct createProduct);
    }
}
