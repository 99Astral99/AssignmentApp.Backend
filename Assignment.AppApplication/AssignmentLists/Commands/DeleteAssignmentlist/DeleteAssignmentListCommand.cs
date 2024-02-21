using FluentResults;
using MediatR;

namespace AssignmentApp.Application.AssignmentLists.Commands.DeleteAssignmentlist
{
    public sealed record DeleteAssignmentListCommand
            (Guid sourceAssignmentListId, Guid destinationAssignmentListId) : IRequest<Result>;
}
