using LMB.Application.DTOs;
using LMB.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LMB.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.LoginData.Email, cancellationToken);

            if (user == null)
            {
                throw new ApplicationException("Invalid credentials.");
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.LoginData.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new ApplicationException("Invalid credentials: Incorrect login or password.");
            }

            var token = _tokenService.CreateToken(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt
            };

            return new AuthResponseDto
            {
                Token = token,
                User = userDto
            };
        }
    }
}
