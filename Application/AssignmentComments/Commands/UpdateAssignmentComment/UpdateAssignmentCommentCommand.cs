using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.AssignmentComments.Commands.UpdateAssignmentComment
{
    public sealed record UpdateAssignmentCommentCommand
        (Guid Id, string Message) : IRequest<Result<ResponseAssignmentComment>>;
}
