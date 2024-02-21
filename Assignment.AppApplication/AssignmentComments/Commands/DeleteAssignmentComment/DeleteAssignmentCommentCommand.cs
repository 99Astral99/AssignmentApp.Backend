using FluentResults;
using MediatR;

namespace AssignmentApp.Application.AssignmentComments.Commands.DeleteAssignmentComment
{
    public sealed record DeleteAssignmentCommentCommand
        (Guid Id) : IRequest<Result>;
}
