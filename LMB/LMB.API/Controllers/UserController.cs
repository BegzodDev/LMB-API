using LMB.Application.DTOs;
using LMB.Application.Features.Users.Commands.RegisterUser;
using LMB.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await _mediator.Send(query);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] RegisterUserRequestDto requestDto)
        {
            var command = new RegisterUserCommand { UserData = requestDto };

            try
            {
                var registeredUser = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetUserById), new { id = registeredUser.Id }, registeredUser);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "User with this email already exists.")
                {
                    return Conflict(new { message = ex.Message });
                }
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }
    }
}
