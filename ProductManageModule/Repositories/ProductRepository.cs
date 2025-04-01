using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Context;
using ProductManagementModule.Domain;

namespace ProductManagementModule.Repositories
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
