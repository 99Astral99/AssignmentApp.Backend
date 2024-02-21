using AssignmentApp.Application.Interfaces;
using AssignmentApp.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Commands.UpdateStatus
{
    public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStatusCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Result> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == request.AssignmentId);

            if (assignment is null)
                return Result.Fail($"Задача с ID {request.AssignmentId} не найдена");

            assignment.UpdateStatus((Status)Enum.Parse(typeof(Status), request.Status));
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
