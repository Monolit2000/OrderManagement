using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
    }

    
}
