using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Orders;
using OrderManagement.Application.Orders.CreateOrder;
using OrderManagement.Application.Orders.GetAllOrders;

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

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }
    }
}
