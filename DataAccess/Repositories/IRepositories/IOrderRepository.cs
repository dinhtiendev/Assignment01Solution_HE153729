using System;
using BussinessObject;

namespace DataAccess.Repositories.IRepositories
{
	public interface IOrderRepository
	{
        Task<IEnumerable<OrderDto>> GetOrders();
        Task<OrderDto> GetOrderById(int OrderId);
        Task<OrderDto> CreateUpdateOrder(OrderDto orderDto);
        Task<bool> DeleteOrder(int orderId);
    }
}

