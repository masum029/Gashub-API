using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.StockFeatures.Handlers.CommandHandlers;
using Project.Application.Features.StockFeatures.Handlers.QueryHandlers;


namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateStock")]
        public async Task<IActionResult> Create(CreateStockCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllStock")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllStockQuery()));
        }
        [HttpGet("getStock/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetStockByIdQuery(id)));
        }
        [HttpDelete("DeleteStock/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteStockCommand(id)));
        }
        [HttpPut("UpdateStock/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateStockCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
