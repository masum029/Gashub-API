using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.AuthFeatures.Commands;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
