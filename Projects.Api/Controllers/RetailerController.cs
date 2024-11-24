using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.RetailerFeatures.Handlers.CommandHandlers;
using Project.Application.Features.RetailerFeatures.Handlers.QueryHandlers;


namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RetailerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateRetailer")]


        public async Task<IActionResult> Create(CreateRetailerCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllRetailer")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllRetailerQuery()));
        }
        [HttpGet("getRetailer/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetRetailerByIdQuery(id)));
        }
        [HttpDelete("DeleteRetailer/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteRetailerCommand(id)));
        }
        [HttpPut("UpdateRetailer/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateRetailerCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
