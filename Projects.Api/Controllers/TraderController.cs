using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.TraderFeatures.Handlers.CommandHandlers;
using Project.Application.Features.TraderFeatures.Handlers.QueryHandlers;


namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TraderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateTrader")]
        public async Task<IActionResult> Create(CreateTraderCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllTrader")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllTraderQuery()));
        }
        [HttpGet("getTrader/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetTraderByIdQuery(id)));
        }
        [HttpDelete("DeleteTrader/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTraderCommand(id)));
        }
        [HttpPut("UpdateTrader/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTraderCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
