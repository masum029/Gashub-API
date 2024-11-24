using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.DeliveryAddressFeatures.Handlers.CommandHandlers;
using Project.Application.Features.DeliveryAddressFeatures.Handlers.QueryHandlers;
using System.Security.Claims;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateDeliveryAddress")]
        public async Task<IActionResult> Create(CreateDeliveryAddressCommand commend)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            commend.CreatedBy = userId ?? "Anonymous ";
            if (commend == null) return BadRequest();
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllDeliveryAddress")]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _mediator.Send(new GetAllDeliveryAddressQuery()));
        }
        [HttpGet("getDeliveryAddress/{id}")]
        public async Task<IActionResult> getById(Guid id)
        {
            return Ok(await _mediator.Send(new GetDeliveryAddressByIdQuery(id)));
        }
        [HttpDelete("DeleteDeliveryAddress/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteDeliveryAddressCommand(id)));
        }
        [HttpPut("UpdateDeliveryAddress/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateDeliveryAddressCommand commend)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            commend.UpdatedBy = userId ?? "Anonymous ";
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
