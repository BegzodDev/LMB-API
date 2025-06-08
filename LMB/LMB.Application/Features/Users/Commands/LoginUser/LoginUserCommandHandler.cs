using LMB.Application.DTOs;
using LMB.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LMB.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto?>
    {
        private readonly AppDbContext _context; 

        public LoginUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.LoginData.Email, cancellationToken);

            if (user == null)
            {
                return null;
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.LoginData.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt
            };

            return userDto;
        }
    }
}
