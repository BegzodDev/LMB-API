using LMB.Application.DTOs;
using MediatR;

namespace LMB.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResponseDto>
    {
        public LoginUserRequestDto LoginData { get; set; } = new LoginUserRequestDto();
    }
}
