using OrderManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Orders
{
    public class Order
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CustomerFullName { get; private set; }
        public string CustomerPhone { get; private set; }
        private readonly List<OrderProduct> _orderProducts = new List<OrderProduct>();
        public IReadOnlyCollection<OrderProduct> OrderProducts => _orderProducts.AsReadOnly();

        public decimal TotalPrice => _orderProducts.Sum(op => op.TotalPrice);

        private Order(string customerFullName, string customerPhone)
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            CustomerFullName = customerFullName ?? throw new ArgumentNullException(nameof(customerFullName));
            CustomerPhone = customerPhone ?? throw new ArgumentNullException(nameof(customerPhone));
        }

        public static Order Create(string customerFullName, string customerPhone)
        {
            return new Order(customerFullName, customerPhone);
        }

        public void AddProduct(Product product, int amount)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            var orderProduct = OrderProduct.Create(this, product, amount);
            _orderProducts.Add(orderProduct);
        }

        public void RemoveProduct(Guid productId)
        {
            var orderProduct = _orderProducts.FirstOrDefault(op => op.ProductId == productId);
            if (orderProduct != null)
            {
                _orderProducts.Remove(orderProduct);
            }
        }
    }
}
