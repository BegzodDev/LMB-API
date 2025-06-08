using LMB.Application.DTOs;
using LMB.Persistence;
using MediatR;

namespace LMB.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly AppDbContext _context;
        public GetUserByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id,cancellationToken);

            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    CreatedAt = user.CreatedAt
                };
            }

            return null;
        }
    }
}
