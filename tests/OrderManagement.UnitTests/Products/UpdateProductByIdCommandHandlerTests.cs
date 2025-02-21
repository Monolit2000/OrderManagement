using Bogus;
using NSubstitute;
using FluentAssertions;
using OrderManagement.Domain.Products;
using OrderManagement.Application.Products.UpdateProductById;
using Xunit;

namespace OrderManagement.UnitTests.Products
{
    public class UpdateProductByIdCommandHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly UpdateProductByIdCommandHandler _handler;

        public UpdateProductByIdCommandHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new UpdateProductByIdCommandHandler(_productRepository);
        }

        [Fact]
        public async Task Handle_ShouldUpdateProduct_WhenProductExistsAndCodeIsUnique()
        {
            // Arrange
            var faker = new Faker();
            var productId = Guid.NewGuid();
            var existingProduct = Product.Create(faker.Random.AlphaNumeric(10), faker.Commerce.ProductName(), faker.Random.Decimal(1, 100));
            var updatedProductCode = "UpdatedCode";

            _productRepository.GetProductByIdAsync(productId).Returns(existingProduct);
            _productRepository.GetProductByCodeAsync(updatedProductCode).Returns((Product)null);

            var command = new UpdateProductByIdCommand
            {
                ProductId = productId,
                Code = updatedProductCode,
                Name = "UpdatedName",
                Price = 25.99M
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ProductId.Should().Be(existingProduct.Id);
            result.Code.Should().Be(updatedProductCode);
            result.Name.Should().Be(command.Name);
            result.Price.Should().Be(command.Price);
            await _productRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepository.GetProductByIdAsync(productId).Returns((Product)null);

            var command = new UpdateProductByIdCommand
            {
                ProductId = productId,
                Code = "Code",
                Name = "Name",
                Price = 20.99M
            };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage($"Product with ID {productId} not found.");
            await _productRepository.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProductCodeAlreadyExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = Product.Create("ExistingCode", "Existing Product", 15.99M);
            var productToUpdate = Product.Create("Code", "Product to Update", 20.99M);

            _productRepository.GetProductByIdAsync(productId).Returns(productToUpdate);
            _productRepository.GetProductByCodeAsync("ExistingCode").Returns(existingProduct);

            var command = new UpdateProductByIdCommand
            {
                ProductId = productId,
                Code = "ExistingCode", 
                Name = "UpdatedName",
                Price = 25.99M
            };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage($"Product with code ExistingCode already exists.");
            await _productRepository.DidNotReceive().SaveChangesAsync();
        }
    }
}
