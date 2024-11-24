﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Features.RoleFeatures.Commands;
using Project.Application.Features.RoleFeatures.Queries;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<ActionResult> CreateRoleAsync(RoleCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<RoleDTO>))]
        public async Task<IActionResult> GetRoleAsync()
        {
            return Ok(await _mediator.Send(new GetRoleQuery()));
        }


        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(RoleDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetRoleByIdQuery() { RoleId = id }));
        }

        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            return Ok(await _mediator.Send(new DeleteRoleCommand()
            {
                RoleId = id
            }));
        }

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditRole(string id, [FromBody] UpdateRoleCommand command)
        {
            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
