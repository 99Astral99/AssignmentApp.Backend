using AssignmentApp.Domain.Entities;

namespace AssignmentApp.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
