using LMB.Application.DTOs;
using MediatR;

namespace LMB.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand: IRequest<UserDto>
    {
        public RegisterUserRequestDto UserData { get; set; } = new RegisterUserRequestDto();
    }
}
