using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
	public class OrderDAO
	{
        private Assignment1APIContext _dbContext;

        public OrderDAO()
        {
            _dbContext = DbContextSingleton.Instance;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders.Include(x => x.OrderDetails).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders.Include(x => x.OrderDetails).Where(x => x.OrderId == orderId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            foreach (var orderDetail in order.OrderDetails)
            {
                _dbContext.OrderDetails.Add(orderDetail);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order oldOrder, Order order)
        {
            _dbContext.Entry(oldOrder).State = EntityState.Detached;
            oldOrder.RequiredDate = order.RequiredDate;
            oldOrder.OrderDate = order.OrderDate;
            oldOrder.ShippedDate = order.ShippedDate;
            oldOrder.Freight = order.Freight;
            _dbContext.OrderDetails.RemoveRange(oldOrder.OrderDetails);
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.OrderId = order.OrderId;
                _dbContext.OrderDetails.Add(orderDetail);
            }
            _dbContext.Orders.Update(oldOrder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            foreach (var orderDetail in order.OrderDetails)
            {
                _dbContext.OrderDetails.Remove(orderDetail);
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}

