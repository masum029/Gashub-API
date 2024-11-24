using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ProductSizeFeatures.Handlers.CommandHandlers;
using Project.Application.Features.ProductSizeFeatures.Handlers.QueryHandlers;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductSizeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateProductSize")]
        public async Task<IActionResult> Create(CreateProductSizeCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllProductSize")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllProductSizeQuery()));
        }
        [HttpGet("getProductSize/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductSizeByIdQuery(id)));
        }
        [HttpDelete("DeleteProductSize/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductSizeCommand(id)));
        }
        [HttpPut("UpdateProductSize/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductSizeCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
