using MediatR;

using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.Orders.GetOrderById
{
    public class GetOrderByIdQueryHandler(
        IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetOrderByIdAsync(request.OrderId);

            if (order == null)
                throw new Exception($"Order with ID {request.OrderId} not found.");

            var orderDto = new OrderDto
            {
                CustomerFullName = order.CustomerFullName,
                CustomerPhone = order.CustomerPhone,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name

                }).ToList()
            };

            return orderDto;   
        }
    }
}
