using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.DTO.Request;
using Restaurant.Application.Commands;
using Restaurant.Application.Queries;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenuItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            var result = await _mediator.Send(new GetAllMenuItemsQuery());
            return Ok(new { Message = "List of menu items", Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var result = await _mediator.Send(new GetMenuItemByIdQuery(id));
            return Ok(new { Message = "Menu item details", Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(CreateMenuItemDto request)
        {
            var result = await _mediator.Send(new CreateMenuItemCommand
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
            });
            return Created(string.Empty, new { Message = "Menu item created successfully", Data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, UpdateMenuItemDto request)
        {
            var result = await _mediator.Send(new UpdateMenuItemCommand
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
            });
            return Ok(new { Message = "Menu item updated successfully", Data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _mediator.Send(new DeleteMenuItemCommand(id));
            if (!result)
            {
                return NotFound(new { Message = "Menu item not found" });
            }
            return Ok(new { Message = "Menu item deleted successfully" });
        }
    }
}