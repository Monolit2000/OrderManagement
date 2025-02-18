using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public string CustomerFullName { get; }
        public string CustomerPhone { get; }
        public List<OrderProductDto> OrderProducts { get; }
    }
}
