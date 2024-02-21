using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentLists.Commands.DeleteAssignmentlist
{
    public class DeleteAssignmentListCommandHandler : IRequestHandler<DeleteAssignmentListCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAssignmentListCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteAssignmentListCommand request, CancellationToken cancellationToken)
        {
            var sourceAssignmentList = await _context.AssignmentLists
                .Include(x => x.Assignments)
                .FirstOrDefaultAsync(x => x.Id == request.sourceAssignmentListId);

            if (sourceAssignmentList == null)
                throw new NotFoundException(nameof(AssignmentList), request.sourceAssignmentListId);

            var destinationAssignmentList = await _context.AssignmentLists
                .FirstOrDefaultAsync(x => x.Id == request.destinationAssignmentListId);

            if (destinationAssignmentList == null)
                throw new NotFoundException(nameof(AssignmentList), request.destinationAssignmentListId);

            if (sourceAssignmentList.Assignments.Any())
            {
                foreach (var assignment in sourceAssignmentList.Assignments)
                {
                    sourceAssignmentList.RemoveAssignment(assignment.Id);
                    destinationAssignmentList.AddAssignment(assignment);
                }
            }
            _context.AssignmentLists.Remove(sourceAssignmentList);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
