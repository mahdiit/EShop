using Eshop.Infrastructure.Command.Product;
using Eshop.Infrastructure.Event.Product;
using EShop.Product.DataProvider.Reporsitories;
using System.Threading.Tasks;

namespace EShop.Product.DataProvider.Services
{
    public class ProductService : IProductService
    {
        IProductRepository ProductRepository;
        public ProductService(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public async Task<ProductCreated> AddProduct(CreateProduct createProduct)
        {

            return await ProductRepository.AddProduct(createProduct);
        }

        public async Task<ProductCreated> GetProduct(string id)
        {
            return await ProductRepository.GetProduct(id);
        }
    }
}
