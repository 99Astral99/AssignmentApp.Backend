using FluentResults;
using MediatR;

namespace AssignmentApp.Application.Assignments.Commands.UpdateStatus
{
    public sealed record UpdateStatusCommand
        (Guid AssignmentId, string Status) : IRequest<Result>;
}
