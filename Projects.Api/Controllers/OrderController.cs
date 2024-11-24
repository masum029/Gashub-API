using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Features.OrderFeatures.Handlers.CommandHandlers;
using Project.Application.Features.OrderFeatures.Handlers.QueryHandlers;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Create(CreateOrderCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpPost("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrder(CreateConfirmOrderComment commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllOrder")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllOrderQuery()));
        }
        [HttpGet("getAllIsPlasedOrder")]
        public async Task<IActionResult> getAllIsPlasedOrder()
        {
            return Ok(await _mediator.Send(new GetAllIsPlasedOrderQuery()));
        }
        [HttpGet("getAllIsConfirmedOrder")]
        public async Task<IActionResult> getAllIsConfirmedOrder()
        {
            return Ok(await _mediator.Send(new GetAllIsConfirmedOrderQuery()));
        }
        [HttpGet("getAllIsRadyToDispatchOrder")]
        public async Task<IActionResult> getAllIsRadyToDispatchOrder()
        {
            return Ok(await _mediator.Send(new GetAllIsRadyToDispatchOrderQuery()));
        }
        [HttpGet("getAllIsDispatchOrder")]
        public async Task<IActionResult> getAllIsDispatchOrder()
        {
            return Ok(await _mediator.Send(new GetAllIsDispatchOrderQuery()));
        }
        [HttpGet("getAllIsDelevarateOrder")]
        public async Task<IActionResult> getAllIsDelevarateOrder()
        {
            return Ok(await _mediator.Send(new GetAllIsDelevarateOrderQuery()));
        }
       
        [HttpGet("getOrder/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
        }
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteOrderCommand(id)));
        }
        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateOrderCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
