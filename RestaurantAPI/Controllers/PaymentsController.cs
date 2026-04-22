using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.DTO.Request;
using Restaurant.Application.Commands;
using Restaurant.Application.Queries;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var result = await _mediator.Send(new GetAllPaymentsQuery());
            return Ok(new { Message = "List of payments", Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var result = await _mediator.Send(new GetPaymentByIdQuery(id));
            return Ok(new { Message = "Payment details", Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto request)
        {
            var result = await _mediator.Send(new CreatePaymentCommand
            {
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TableId = request.TableId,
            });
            return Created(string.Empty, new { Message = "Payment created successfully", Data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDto request)
        {
            var result = await _mediator.Send(new UpdatePaymentCommand
            {
                Id = id,
                Amount = request.Amount,
                IsSuccessful = request.IsSuccessful,
                PaymentMethod = request.PaymentMethod,
                TableId = request.TableId,
            });
            return Ok(new { Message = "Payment updated successfully", Data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _mediator.Send(new DeletePaymentCommand(id));
            if (!result)
            {
                return NotFound(new { Message = "Payment not found" });
            }
            return Ok(new { Message = "Payment deleted successfully" });
        }
    }
}