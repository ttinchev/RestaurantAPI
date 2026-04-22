using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.DTO.Request;
using Restaurant.Application.Commands;
using Restaurant.Application.Queries;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TablesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var result = await _mediator.Send(new GetAllTablesQuery());
            return Ok(new { Message = "List of tables", Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var result = await _mediator.Send(new GetTableByIdQuery(id));
            return Ok(new { Message = "Table details", Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(CreateTableDto request)
        {
            var result = await _mediator.Send(new CreateTableCommand
            {
                NumberOfSeats = request.NumberOfSeats,
                IsFree = request.IsFree,
                Enabled = request.Enabled,
            });
            return Created(string.Empty, new { Message = "Table created successfully", Data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, UpdateTableDto request)
        {
            var result = await _mediator.Send(new UpdateTableCommand
            {
                Id = id,
                NumberOfSeats = request.NumberOfSeats,
                IsFree = request.IsFree,
                Enabled = request.Enabled,
            });
            return Ok(new { Message = "Table updated successfully", Data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _mediator.Send(new DeleteTableCommand(id));
            if (!result)
            {
                return NotFound(new { Message = "Table not found" });
            }
            return Ok(new { Message = "Table deleted successfully" });
        }
    }
}