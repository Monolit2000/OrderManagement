using MediatR;
using OrderManagement.Domain.Products;


namespace OrderManagement.Application.Products.UpdateProductById
{
    public class UpdateProductByIdCommandHandler(
        IProductRepository productRepository) : IRequestHandler<UpdateProductByIdCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(UpdateProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
                throw new Exception($"Product with ID {request.ProductId} not found.");
    
            var existingProduct = await productRepository.GetProductByCodeAsync(request.Code);
            if (existingProduct != null && product.Id != existingProduct.Id)
                throw new Exception($"Product with code {request.Code} already exists.");

            product.Update(request.Code, request.Name, request.Price);  

            await productRepository.SaveChangesAsync();

            var productDto = new ProductDto
            {
                ProductId = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            };

            return productDto;
        }
    }
}
