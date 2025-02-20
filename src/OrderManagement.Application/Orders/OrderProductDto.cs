using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Orders
{
    public class OrderProductDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be at least 1.")]
        public int Amount { get; set; }

        public string ProductName { get; set; }
    }
}
