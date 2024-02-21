using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.AssignmentLists.Queries.GetAssignmentList
{
    public sealed record GetAssignmentListQuery
         (Guid Id) : IRequest<Result<ResponseAssignmentList>>;
}
