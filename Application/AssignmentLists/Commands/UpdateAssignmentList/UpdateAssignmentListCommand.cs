using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.AssignmentLists.Commands.UpdateAssignmentList
{
    public sealed record UpdateAssignmentListCommand
            (Guid Id, string Name, string Description) : IRequest<Result<ResponseAssignmentList>>;

}
