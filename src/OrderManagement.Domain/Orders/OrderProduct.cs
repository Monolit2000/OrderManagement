using OrderManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Orders
{
    public class OrderProduct
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderProduct() { } //For EF Core

        private OrderProduct(Order order, Product product, int amount)
        {
            Id = Guid.NewGuid();
            Order = order ?? throw new ArgumentNullException(nameof(order));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Amount = amount > 0 ? amount : throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        public static OrderProduct Create(Order order, Product product, int amount)
        {
            return new OrderProduct(order, product, amount);
        }
    }
}
