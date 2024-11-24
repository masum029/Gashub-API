using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ProdReturnFeatures.Handlers.CommandHandlers;
using Project.Application.Features.ProdReturnFeatures.Handlers.QueryHandlers;
using Project.Application.Features.ProductDiscuntFeatures.Handlers.CommandHandlers;
using Project.Application.Features.ProductDiscuntFeatures.Handlers.QueryHandlers;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDiscuntController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductDiscuntController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateProductDiscunt")]
        public async Task<IActionResult> Create(CreateProductDiscuntCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllProductDiscunt")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllProductDiscuntQuery()));
        }
        [HttpGet("getProductDiscunt/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductDiscuntByIdQuery(id)));
        }
        [HttpDelete("DeleteProductDiscunt/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductDiscuntCommand(id)));
        }
        [HttpPut("UpdateProductDiscunt/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDiscuntCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
