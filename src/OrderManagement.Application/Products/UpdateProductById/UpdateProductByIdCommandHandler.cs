using MediatR;
using OrderManagement.Application.Products.GetAllProducts;
using OrderManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.UpdateProductById
{
    public class UpdateProductByIdCommandHandler(
        IProductRepository productRepository) : IRequestHandler<UpdateProductByIdCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(UpdateProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
                throw new Exception($"Product with ID {request.Id} not found.");

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
