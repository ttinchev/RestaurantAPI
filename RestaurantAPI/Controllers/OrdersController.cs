using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.DTO.Request;
using Restaurant.Application.Commands;
using Restaurant.Application.Models.Order;
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
    }
}
