using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ProdReturnFeatures.Handlers.CommandHandlers;
using Project.Application.Features.ProdReturnFeatures.Handlers.QueryHandlers;


namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdReturnController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdReturnController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateProdReturn")]
        public async Task<IActionResult> Create(CreateProdReturnCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllProdReturn")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllProdReturnQuery()));
        }
        [HttpGet("getAllConfirmCustomer")]
        public async Task<IActionResult> getAllConfirmCustomer()
        {
            return Ok(await _mediator.Send(new GetAllConfirmProdReturnQuery()));
        }
        [HttpGet("getProdReturn/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetProdReturnByIdQuery(id)));
        }
        [HttpDelete("DeleteProdReturn/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProdReturnCommand(id)));
        }
        [HttpPut("UpdateProdReturn/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProdReturnCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
