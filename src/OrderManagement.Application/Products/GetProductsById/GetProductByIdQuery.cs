using MediatR;
using OrderManagement.Application.Products.GetAllProducts;


namespace OrderManagement.Application.Products.GetProductsById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid ProductId { get; set; }
    }
}
