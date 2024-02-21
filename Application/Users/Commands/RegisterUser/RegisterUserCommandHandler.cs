using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Interfaces.Auth;
using AssignmentApp.Domain.Entities;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var userExist = _context.Users.FirstOrDefault(x => x.Email == request.Email);

            var hashedPassword = _passwordHasher.Generate(request.Password);

            if (userExist is not null)
                return Result.Fail("Пользователь с такими данными уже зарегистрирован");

            var user = User.Create(Guid.NewGuid(), request.Name, request.Email, hashedPassword).Value;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
