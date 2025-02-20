using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Products.CreateProduct;
using OrderManagement.Application.Products.GetAllProducts;
using OrderManagement.Application.Products.GetProductsByCode;
using OrderManagement.Application.Products.GetProductsById;
using OrderManagement.Application.Products.UpdateProductById;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductByIdCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }


        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { ProductId = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetProductsByCode/{code}")]
        public async Task<IActionResult> GetProductsByCode(string code)
        {
            var result = await _mediator.Send(new GetProductsByCodeQuery { Code = code });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
