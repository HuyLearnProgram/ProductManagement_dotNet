// ProductService.cs (Business Logic with Async/Await)
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ProductManagementModule
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(long id)
        {
            return _productRepository.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(long id)
        {
            _productRepository.DeleteProduct(id);
        }
    }
}