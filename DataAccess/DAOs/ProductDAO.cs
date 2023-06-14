using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
	public class ProductDAO
	{
        private Assignment1APIContext _dbContext;

        public ProductDAO()
        {
            _dbContext = DbContextSingleton.Instance;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dbContext.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product oldProduct, Product product)
        {
            _dbContext.Entry(oldProduct).State = EntityState.Detached;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _dbContext.OrderDetails.RemoveRange(product.OrderDetails);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

