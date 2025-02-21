using MediatR;
using OrderManagement.Domain.Products;

namespace OrderManagement.Application.Products.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductRepository productRepository ) : IRequestHandler<CreateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var isProductCodeExist = await productRepository.GetProductByCodeAsync(request.Code);

            if (isProductCodeExist != null)
                throw new Exception($"Product with code {request.Code} already exists.");   

            var product = Product.Create(request.Code, request.Name, request.Price);

            await productRepository.AddProductAsync(product);

            await productRepository.SaveChangesAsync();

            var createProductDto = new ProductDto
            {
                ProductId = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            };

            return createProductDto;
        }
    }
}
