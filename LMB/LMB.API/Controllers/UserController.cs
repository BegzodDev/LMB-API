using LMB.Application.Features.Users.Commands.RegisterUser;
using LMB.Application.Features.Users.Queries.GetUserById;
using LMB.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] RegisterUserRequestDto requestDto)
        {
            var command = new RegisterUserCommand { UserData = requestDto };

            var registeredUser = await _sender.Send(command);
            Console.WriteLine(registeredUser);
            return CreatedAtAction(nameof(GetUserById), new { id = registeredUser.Id }, registeredUser);
            //try
            //{
            //}
            //catch (ApplicationException ex)
            //{
            //    if (ex.Message == "User with this email already exists.")
            //    {
            //        return Conflict(new { message = ex.Message });
            //    }
            //    return BadRequest(new { message = ex.Message });
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            //}
        }
    }
}
