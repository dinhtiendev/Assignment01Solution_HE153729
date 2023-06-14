using System;
using AutoMapper;
using BussinessObject;
using DataAccess.DAOs;
using DataAccess.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
	public class OrderRepository : IOrderRepository
	{
        private OrderDAO _orderDAO;
        private IMapper _mapper;

        public OrderRepository(OrderDAO orderDAO, IMapper mapper)
        {
            _orderDAO = orderDAO;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            IEnumerable<Order> OrderList = await _orderDAO.GetOrdersAsync();
            return _mapper.Map<List<OrderDto>>(OrderList);
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            Order order = await _orderDAO.GetOrderByIdAsync(orderId);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateUpdateOrder(OrderDto orderDto)
        {
            Order order = _mapper.Map<OrderDto, Order>(orderDto);
            Order oldOrder = await _orderDAO.GetOrderByIdAsync(order.OrderId);
            if (oldOrder != null)
            {
                await _orderDAO.UpdateAsync(oldOrder, order);
            }
            else
            {
                await _orderDAO.CreateAsync(order);
            }
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            try
            {
                Order order = await _orderDAO.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return false;
                }
                await _orderDAO.DeleteAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

