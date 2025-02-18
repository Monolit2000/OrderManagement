using OrderManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Orders
{
    public interface IOrderRepository
    {
        // Create methods
        Task<Order> AddOrderAsync(Order order);

        // Read methods
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
