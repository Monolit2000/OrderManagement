using MediatR;
using OrderManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductRepository productRepository ) : IRequestHandler<CreateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Code, request.Name, request.Price);

            await productRepository.AddProductAsync(product);

            await productRepository.SaveChangesAsync();

            var createProductDto = new ProductDto
            {
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            };

            return createProductDto;
        }
    }
}
