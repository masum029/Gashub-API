using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.CompanyFeatures.Handlers.CommandHandlers;
using Project.Application.Features.CompanyFeatures.Handlers.QueryHandlers;
using System.Security.Claims;
namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateCompany")]
        public async Task<IActionResult> Create(CreateCompanyCommand commend)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            commend.CreatedBy = userId ?? "Anonymous ";
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllCompany")]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _mediator.Send(new GetAllCompanyQuery()));
        }
        [HttpGet("getCompany/{id}")]
        public async Task<IActionResult> getById(Guid id)
        {
            return Ok(await _mediator.Send(new GetCompanyByIdQuery(id)));
        }
        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCompanyCommand(id)));
        }
        [HttpPut("UpdateCompany/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCompanyCommand commend)
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
