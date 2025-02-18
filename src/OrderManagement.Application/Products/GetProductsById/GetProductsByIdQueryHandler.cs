using MediatR;
using OrderManagement.Application.Products.GetAllProducts;
using OrderManagement.Domain.Products;

namespace OrderManagement.Application.Products.GetProductsById
{
    public class GetProductsByIdQueryHandler(
        IProductRepository productRepository) : IRequestHandler<GetProductsByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByIdAsync(request.ProductId);

            if (product == null)
                throw new Exception($"Product with ID {request.ProductId} not found.");

            var productDto = new ProductDto
            {
                Id = product.Id,    
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            };

            return productDto;
        }
    }
}
