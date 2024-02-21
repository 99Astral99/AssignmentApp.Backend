using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.Application.AssignmentLists.Commands.CreateAssignmentList
{
    public sealed record CreateAssignmentListCommand
        ([Required] string Name, string Description, Guid UserId) : IRequest<Result<ResponseAssignmentList>>;
}
