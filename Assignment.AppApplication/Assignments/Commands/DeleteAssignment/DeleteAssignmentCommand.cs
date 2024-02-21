using FluentResults;
using MediatR;

namespace AssignmentApp.Application.Assignments.Commands.DeleteAssignment
{
    public record DeleteAssignmentCommand
        (Guid AssignmentId, Guid AssignmentListId) : IRequest<Result>;
}
