using LMB.Domain;

namespace LMB.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
