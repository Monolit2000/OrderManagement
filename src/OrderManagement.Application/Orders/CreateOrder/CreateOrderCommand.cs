using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string CustomerFullName { get; set; }

        [Required]
        //[Phone]
        public string CustomerPhone { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one product.")]
        public List<OrderProductDto> OrderProducts { get; set; } = [];
    }
}
