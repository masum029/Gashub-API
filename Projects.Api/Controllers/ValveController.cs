using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ValveFeatures.Handlers.CommandHandlers;
using Project.Application.Features.ValveFeatures.Handlers.QueryHandlers;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValveController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValveController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateValve")]
        public async Task<IActionResult> Create(CreateValveCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllValve")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllValveQuery()));
        }
        [HttpGet("getValve/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetValveByIdQuery(id)));
        }
        [HttpDelete("DeleteValve/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteValveCommand(id)));
        }
        [HttpPut("UpdateValve/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateValveCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
