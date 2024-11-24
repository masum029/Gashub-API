using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ProductFeatures.Handlers.CommandHandlers;
using Project.Application.Features.ProductFeatures.Handlers.QueryHandlers;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(CreateProductCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllProduct")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllProductQuery()));
        }
        [HttpGet("getProduct/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
