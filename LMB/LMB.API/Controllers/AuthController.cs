using LMB.Application.DTOs;
using LMB.Application.Features.Users.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISender _sender;

        public AuthController(IMediator mediator, ISender sender)
        {
            _mediator = mediator;
            _sender = sender;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserRequestDto requestDto)
        {
            var command = new LoginUserCommand { LoginData = requestDto };
            var user = await _sender.Send(command);
            return Ok(user);

        }
    }
}
