using OrderManagement.Domain.Orders;
using OrderManagement.Domain.Products;
using Bogus;

namespace OrderManagement.UnitTests
{
    public class TestBase
    {
        protected class OrderManagementTestDataOptions
        {
            public string CustomerFullName { get; set; }
            public string ProductCode { get; set; }
            public string CustomerPhone { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; } = 1; // Установите значение по умолчанию больше нуля
        }

        protected class OrderManagementTestData
        {
            public Order Order { get; set; }
            public Product Product { get; set; }

            public OrderManagementTestData(Order order, Product product)
            {
                Order = order;
                Product = product;
            }
        }

        protected OrderManagementTestData CreateOrderManagementTestData(OrderManagementTestDataOptions options = null)
        {
            var faker = new Faker();

            options ??= new OrderManagementTestDataOptions();

            var productCode = options.ProductCode ?? faker.Commerce.Product();
            var productName = options.ProductName ?? faker.Commerce.ProductName();
            var price = options.Price > 0 ? options.Price : faker.Random.Decimal(1, 1000); 
            var customerFullName = options.CustomerFullName ?? faker.Name.FullName();
            var customerPhone = options.CustomerPhone ?? faker.Phone.PhoneNumber();
            var quantity = options.Quantity > 0 ? options.Quantity : faker.Random.Int(1, 10); 

            var product = Product.Create(productCode, productName, price);
            var order = Order.Create(customerFullName, customerPhone);
            order.AddProduct(product, quantity);

            return new OrderManagementTestData(order, product);
        }
    }
}
