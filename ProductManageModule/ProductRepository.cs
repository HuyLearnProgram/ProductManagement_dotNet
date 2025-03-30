//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProductManagementModule
//{
//    public class ProductRepository : IProductRepository
//    {
//        private readonly ProductDbContext _context;

//        public ProductRepository(ProductDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Product>> GetAllProducts()
//        {
//            return await _context.products.ToListAsync();
//        }

//        public async Task<Product> GetProductById(long id)
//        {
//            return await _context.products.FindAsync(id);
//        }

//        public async Task AddProduct(Product product)
//        {
//            _context.products.Add(product);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateProduct(Product product)
//        {
//            _context.products.Update(product);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteProduct(long id)
//        {
//            var product = await _context.products.FindAsync(id);
//            if (product != null)
//            {
//                _context.products.Remove(product);
//                await _context.SaveChangesAsync();
//            }
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ProductManagementModule
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.products.ToList();
        }

        public Product GetProductById(long id)
        {
            return _context.products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _context.products.Find(updatedProduct.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(long id)
        {
            var product = _context.products.Find(id);
            if (product != null)
            {
                _context.products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
