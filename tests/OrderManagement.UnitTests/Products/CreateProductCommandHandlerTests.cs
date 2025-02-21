using Bogus;
using NSubstitute;
using FluentAssertions;
using OrderManagement.Domain.Products;
using OrderManagement.Application.Products.CreateProduct;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace OrderManagement.UnitTests.Products
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new CreateProductCommandHandler(_productRepository);
        }

        [Fact]
        public async Task Handle_ShouldCreateProduct_WhenValidRequest()
        {
            // Arrange
            var faker = new Faker();

            var command = new CreateProductCommand
            {
                Code = faker.Random.AlphaNumeric(10),
                Name = faker.Commerce.ProductName(),
                Price = faker.Random.Decimal(1, 1000)
            };

            _productRepository.GetProductByCodeAsync(command.Code).Returns((Product)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Code.Should().Be(command.Code);
            result.Name.Should().Be(command.Name);
            result.Price.Should().Be(command.Price);
            await _productRepository.Received(1).AddProductAsync(Arg.Is<Product>(p =>
                p.Code == command.Code && p.Name == command.Name && p.Price == command.Price));
            await _productRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProductCodeAlreadyExists()
        {
            // Arrange
            var faker = new Faker();
            var existingProduct = Product.Create(faker.Random.AlphaNumeric(10), faker.Commerce.ProductName(), faker.Random.Decimal(1, 100));

            var command = new CreateProductCommand
            {
                Code = existingProduct.Code,
                Name = "New Product",
                Price = 20.99M
            };

            _productRepository.GetProductByCodeAsync(existingProduct.Code).Returns(existingProduct);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage($"Product with code {existingProduct.Code} already exists.");
            await _productRepository.DidNotReceive().AddProductAsync(Arg.Any<Product>());
            await _productRepository.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProductCreationFails()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Code = "UniqueCode",
                Name = "Test Product",
                Price = 20.99M
            };

            _productRepository.GetProductByCodeAsync(command.Code).Returns((Product)null);

            _productRepository.AddProductAsync(Arg.Any<Product>()).Throws(new Exception("Product creation failed"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Product creation failed");
            await _productRepository.Received(1).AddProductAsync(Arg.Any<Product>());
            await _productRepository.DidNotReceive().SaveChangesAsync();
        }
    }
}
