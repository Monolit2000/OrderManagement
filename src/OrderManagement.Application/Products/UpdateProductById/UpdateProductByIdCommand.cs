using MediatR;
using OrderManagement.Application.Products.GetAllProducts;
using System.ComponentModel.DataAnnotations;


namespace OrderManagement.Application.Products.UpdateProductById
{
    public class UpdateProductByIdCommand : IRequest<ProductDto>
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Code { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}
