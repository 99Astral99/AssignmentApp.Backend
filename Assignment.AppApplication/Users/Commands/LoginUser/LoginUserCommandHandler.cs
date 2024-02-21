using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Interfaces.Auth;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(IApplicationDbContext context, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
                return Result.Fail($"Пользователь с email {request.Email} не найден");

            var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (result == false)
                return Result.Fail($"Не удалось войти");

            var token = _jwtProvider.Generate(user);

            return Result.Ok(token);
        }
    }
}
