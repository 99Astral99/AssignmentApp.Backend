using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.Application.Assignments.Commands.UpdateAssignment
{
    public sealed record UpdateAssignmentCommand
        (Guid AssignmentId, Guid AssignmentListId,
        [Required] string Name,
        string? Description) : IRequest<Result<ResponseAssignment>>;
}
