using Microsoft.EntityFrameworkCore;
using LMB.Application.DTOs;
using LMB.Persistence;
using LMB.Domain;
using MediatR;

namespace LMB.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly AppDbContext _context;

        public RegisterUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                                             .AsNoTracking() 
                                             .FirstOrDefaultAsync(u => u.Email == request.UserData.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new ApplicationException("User with this email already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.UserData.Password, 12);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.UserData.Email,
                PasswordHash = hashedPassword,
                FullName = request.UserData.FullName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);

            var newProfile = new LinkProfile
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                Username = request.UserData.Email.Split('@')[0],
                Bio = "Hello there! This is my new link-in-bio page.",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.LinkProfiles.Add(newProfile);
            await _context.SaveChangesAsync(cancellationToken);

            var userDto = new UserDto
            {
                Id = newUser.Id,
                Email = newUser.Email,
                FullName = newUser.FullName,
                CreatedAt = newUser.CreatedAt
            };

            return userDto;
        }
    }
}
