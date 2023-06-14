using System;
using BussinessObject;

namespace DataAccess.Repositories.IRepositories
{
	public interface ICategoryRepository
	{
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int categoryId);
    }
}

