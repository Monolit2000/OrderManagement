using MediatR;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.Orders.GetAllOrders
{
    public class GetAllOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetAllOrdersAsync();

            var orderDtos = orders.Select(order => new OrderDto
            {
                CustomerFullName = order.CustomerFullName,
                CustomerPhone = order.CustomerPhone,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name   

                }).ToList()
            }).ToList();

            return orderDtos;
        }
    }
}
