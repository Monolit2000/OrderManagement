using Bogus;
using NSubstitute;
using FluentAssertions;
using OrderManagement.Domain.Orders;
using OrderManagement.Domain.Products;
using OrderManagement.Application.Orders;
using OrderManagement.Application.Orders.CreateOrder;
using AutoFixture;

namespace OrderManagement.UnitTests.Orders
{
    public class CreateOrderCommandHandlerTests : TestBase
    {
        private readonly Fixture _fixture;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _fixture = new Fixture();
            _orderRepository = Substitute.For<IOrderRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new CreateOrderCommandHandler(_orderRepository, _productRepository);
        }

        [Fact]
        public async Task Handle_ShouldCreateOrder_WhenValidRequest()
        {
            // Arrange
            var faker = new Faker();

            var testData = CreateOrderManagementTestData();

            var productId = Guid.NewGuid();
            var product = CreateOrderManagementTestData().Product;

            _productRepository.GetProductByIdAsync(Arg.Any<Guid>()).Returns(product);



            var command = new CreateOrderCommand
            {
                CustomerFullName = faker.Name.FullName(),
                CustomerPhone = faker.Phone.PhoneNumber(),
                OrderProducts = new List<OrderProductDto>
                    {
                        new OrderProductDto
                        {
                            ProductId = productId,
                            Amount = faker.Random.Int(1, 10)
                        }
                    }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.CustomerFullName.Should().Be(command.CustomerFullName);
            result.CustomerPhone.Should().Be(command.CustomerPhone);
            result.OrderProducts.Should().HaveCount(1);
            await _orderRepository.Received(1).AddOrderAsync(Arg.Any<Order>());
            await _orderRepository.Received(1).SaveChangesAsync();
        }


        [Fact]
        public async Task Handle_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange
            var faker = new Faker();
            var product = CreateOrderManagementTestData().Product;
            _productRepository.GetProductByIdAsync(product.Id).Returns((Product)null);

            var command = new CreateOrderCommand
            {
                CustomerFullName = faker.Name.FullName(),
                CustomerPhone = faker.Phone.PhoneNumber(),
                OrderProducts = new List<OrderProductDto>
                    {
                        new OrderProductDto
                        {
                            ProductId = product.Id,
                            Amount = faker.Random.Int(1, 10)
                        }
                    }
            };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>();
            await _orderRepository.DidNotReceive().AddOrderAsync(Arg.Any<Order>());
            await _orderRepository.DidNotReceive().SaveChangesAsync();
        }
    }
}
