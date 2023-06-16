using System;
using BussinessObject;
using eSroteClient.Services.IServices;

namespace eSroteClient.Services
{
	public class OrderService : BaseService, IOrderService
	{
        private readonly IHttpClientFactory _clientFactory;

        public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateOrderAsync<T>(OrderDto orderDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = orderDto,
                Url = SD.eStoreAPIBase + "/api/Orders",
                AccessToken = token
            });
        }

        public async Task<T> DeleteOrderAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.eStoreAPIBase + "/api/Orders/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllOrdersAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase + "/api/Orders",
                AccessToken = token
            });
        }

        public async Task<T> GetOrderByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase + "/api/Orders/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateOrderAsync<T>(OrderDto orderDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderDto,
                Url = SD.eStoreAPIBase + "/api/Orders",
                AccessToken = token
            });
        }
    }
}

