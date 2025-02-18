using MediatR;
using OrderManagement.Application.Products.GetAllProducts;


namespace OrderManagement.Application.Products.GetProductsById
{
    public class GetProductsByIdQuery : IRequest<ProductDto>
    {
        public Guid ProductId { get; set; }
    }
}
