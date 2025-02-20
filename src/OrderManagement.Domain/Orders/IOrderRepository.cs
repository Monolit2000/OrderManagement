namespace OrderManagement.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);

        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
