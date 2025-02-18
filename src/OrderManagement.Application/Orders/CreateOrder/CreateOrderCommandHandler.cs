using MediatR;
using OrderManagement.Domain.Orders;
using OrderManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.CreateOrder
{
    public class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository) : IRequestHandler<CreateOrderCommand, OrderDto>
    {

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.CustomerFullName, request.CustomerPhone);

            foreach (var orderProductDto in request.OrderProducts)
            {
                var product = await productRepository.GetProductByIdAsync(orderProductDto.ProductId);

                if(product == null)
                    throw new Exception($"Product with ID {orderProductDto.ProductId} not found.");

                order.AddProduct(product, orderProductDto.Amount);
            }

            await orderRepository.AddOrderAsync(order);
            await orderRepository.SaveChangesAsync();

            var createOrderDto = new OrderDto
            {
                CustomerFullName = order.CustomerFullName,
                CustomerPhone = order.CustomerPhone,
                OrderProducts = order.OrderProducts
                .Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name,  
                }).ToList()
            };

            return createOrderDto;
        }
    }
}
