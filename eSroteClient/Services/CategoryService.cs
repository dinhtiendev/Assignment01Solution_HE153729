using System;
using eSroteClient.Services.IServices;

namespace eSroteClient.Services
{
	public class CategoryService : BaseService, ICategoryService
	{
        private readonly IHttpClientFactory _clientFactory;

        public CategoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAllCategoriesAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase + "/api/Categories",
                AccessToken = token
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase + "/api/Categories/" + id,
                AccessToken = token
            });
        }
    }
}

