using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.Application.Assignments.Commands.CreateAssignment
{
    public sealed record CreateAssignmentCommand
           (Guid AssignmentListId, [Required] string Name, string? Description) : IRequest<Result<ResponseAssignment>>;
}
