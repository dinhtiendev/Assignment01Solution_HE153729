using System;
using BussinessObject;

namespace eSroteClient.Services.IServices
{
	public interface IOrderService
	{
        Task<T> GetAllOrdersAsync<T>(string token);
        Task<T> GetOrderByIdAsync<T>(int id, string token);
        Task<T> CreateOrderAsync<T>(OrderDto orderDto, string token);
        Task<T> UpdateOrderAsync<T>(OrderDto orderDto, string token);
        Task<T> DeleteOrderAsync<T>(int id, string token);
    }
}

