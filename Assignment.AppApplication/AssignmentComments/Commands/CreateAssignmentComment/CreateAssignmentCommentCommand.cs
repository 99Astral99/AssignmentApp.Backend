using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.AssignmentComments.Commands.CreateAssignmentComment
{
    public sealed record CreateAssignmentCommentCommand
           (Guid AssignmentId, string? Message) : IRequest<Result<ResponseAssignmentComment>>;
}
