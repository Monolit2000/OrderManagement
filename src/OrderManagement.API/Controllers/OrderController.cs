using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Orders;
using OrderManagement.Application.Orders.CreateOrder;
using OrderManagement.Application.Orders.GetAllOrders;
using OrderManagement.Application.Orders.GetOrderById;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("getAllOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }

        [HttpGet("getOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery { OrderId = id };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
