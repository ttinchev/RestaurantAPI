using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.DTO.Request;
using Restaurant.Application.Commands;
using Restaurant.Application.Queries;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(new { Message = "List of categories", Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(new { Message = "Category details", Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto request)
        {
            var result = await _mediator.Send(new CreateCategoryCommand
            {
                Name = request.Name,
            });
            return Created(string.Empty, new { Message = "Category created successfully", Data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto request)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand
            {
                Id = id,
                Name = request.Name,
            });
            return Ok(new { Message = "Category updated successfully", Data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!result)
            {
                return NotFound(new { Message = "Category not found" });
            }
            return Ok(new { Message = "Category deleted successfully" });
        }
    }
}