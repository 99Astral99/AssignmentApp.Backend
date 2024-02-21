using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.Assignments.Commands.MoveAssignmentToList
{
    public sealed record MoveAssignmentToListCommand
            (Guid AssignmentId, Guid AssignmentListId, Guid newAssignmentListId) : IRequest<Result<ResponseAssignment>>;
}
