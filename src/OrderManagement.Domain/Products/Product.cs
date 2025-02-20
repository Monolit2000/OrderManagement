using OrderManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Products
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product() { } //For EF Core  

        private Product(string code, string name, decimal price)
        {
            Id = Guid.NewGuid();    
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price > 0 ? price : throw new ArgumentException("Price must be greater than zero.", nameof(price));
        }

        public static Product Create(string code, string name, decimal price)
        {
            return new Product(code, name, price);
        }

        public void Update(string code, string name, decimal price)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price > 0 ? price : throw new ArgumentException("Price must be greater than zero.", nameof(price));
        }   
    }
}
