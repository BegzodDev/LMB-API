using LMB.Application.DTOs;
using MediatR;

namespace LMB.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserDto?>
    {
        public LoginUserRequestDto LoginData { get; set; } = new LoginUserRequestDto();
    }
}
