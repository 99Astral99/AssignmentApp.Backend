using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentComments.Commands.DeleteAssignmentComment
{
    public class DeleteAssignmentCommentCommandHandler : IRequestHandler<DeleteAssignmentCommentCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAssignmentCommentCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteAssignmentCommentCommand request, CancellationToken cancellationToken)
        {
            var existAssignmentComment = await _context.AssignmentComments.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existAssignmentComment is null)
                throw new NotFoundException(nameof(AssignmentComment), request.Id);

            _context.AssignmentComments.Remove(existAssignmentComment);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
