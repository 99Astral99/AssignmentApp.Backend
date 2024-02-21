using AssignmentApp.Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Result> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == x.Email);

            if (user == null)
                return Result.Fail("Пользователь не найден");

            _context.Users.Update(user);

            return Result.Ok();
        }
    }
}
