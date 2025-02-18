using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders
{
    public class OrderProductDto
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }

        public string ProductName { get; set; }
    }
}
