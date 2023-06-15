using System;
using BussinessObject;
using DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/Categories")]
    [Authorize]
    public class CategoryAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private ICategoryRepository _categoryRepository;

        public CategoryAPIController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<CategoryDto> categoryDtos = await _categoryRepository.GetCategories();
                _response.Result = categoryDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                CategoryDto categoryDto = await _categoryRepository.GetCategoryById(id);
                _response.Result = categoryDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}

