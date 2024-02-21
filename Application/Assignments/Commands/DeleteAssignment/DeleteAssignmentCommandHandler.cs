using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Commands.DeleteAssignment
{
    public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAssignmentCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteAssignmentCommand request,
            CancellationToken cancellationToken)
        {
            var assignmentList = await _context
                .AssignmentLists
                .Include(x => x.Assignments)
                .SingleOrDefaultAsync(x => x.Id == request.AssignmentListId, cancellationToken);

            if (assignmentList is null)
                throw new NotFoundException(nameof(assignmentList), request.AssignmentListId);

            var assignmentToRemove = assignmentList.Assignments.FirstOrDefault(x => x.Id == request.AssignmentId);

            if (assignmentToRemove is null)
                throw new NotFoundException(nameof(Assignment), request.AssignmentId);

            assignmentList.RemoveAssignment(assignmentToRemove.Id);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
