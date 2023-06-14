using System;
using AutoMapper;
using BussinessObject;
using DataAccess.DAOs;
using DataAccess.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
        private CategoryDAO _categoryDAO;
        private IMapper _mapper;

        public CategoryRepository(CategoryDAO categoryDAO, IMapper mapper)
        {
            _categoryDAO = categoryDAO;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            IEnumerable<Category> categoryList = await _categoryDAO.GetCategorysAsync();
            return _mapper.Map<List<CategoryDto>>(categoryList);
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            Category Category = await _categoryDAO.GetCategoryByIdAsync(categoryId);
            return _mapper.Map<CategoryDto>(Category);
        }
    }
}

