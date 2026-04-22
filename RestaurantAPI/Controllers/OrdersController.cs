using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.DTO.Request;
using Restaurant.Application.Commands;
using Restaurant.Application.Queries;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(new { Message = "List of orders", Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(new { Message = "Order details", Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto request)
        {
            var result = await _mediator.Send(new CreateOrderCommand
            {
                TableId = request.TableId,
                OrderItems = request.OrderItems,
            });
            return Created(string.Empty, new { Message = "Order created successfully", Data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto request)
        {
            var result = await _mediator.Send(new UpdateOrderCommand
            {
                Id = id,
                TableId = request.TableId,
                OrderItems = request.OrderItems,
            });
            return Ok(new { Message = "Order updated successfully", Data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            if (!result)
            {
                return NotFound(new { Message = "Order not found" });
            }
            return Ok(new { Message = "Order deleted successfully" });
        }
    }
}
