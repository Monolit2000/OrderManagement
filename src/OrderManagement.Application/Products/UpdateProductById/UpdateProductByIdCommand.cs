using MediatR;
using OrderManagement.Application.Products.GetAllProducts;


namespace OrderManagement.Application.Products.UpdateProductById
{
    public class UpdateProductByIdCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
