using LMB.Application.Features.Users.Commands.RegisterUser;
using LMB.Application.Features.Users.Queries.GetUserById;
using LMB.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LMB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISender _sender;
        public UserController(IMediator mediator, ISender sender)
        {
            _mediator = mediator;
            _sender = sender;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await _sender.Send(query);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] RegisterUserRequestDto requestDto)
        {
            var command = new RegisterUserCommand { UserData = requestDto };

            var registeredUser = await _sender.Send(command);
            Console.WriteLine(registeredUser);
            return CreatedAtAction(nameof(GetUserById), new { id = registeredUser.Id }, registeredUser);
        }
    }
}
