using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
	public class CategoryDAO
	{
        private Assignment1APIContext _dbContext;

        public CategoryDAO()
        {
            _dbContext = DbContextSingleton.Instance;
        }

        public async Task<IEnumerable<Category>> GetCategorysAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _dbContext.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
        }
    }
}

