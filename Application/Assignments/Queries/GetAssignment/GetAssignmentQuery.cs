using AssignmentApp.Application.Responses;
using FluentResults;
using MediatR;

namespace AssignmentApp.Application.Assignments.Queries.GetAssignment
{
    public sealed record GetAssignmentQuery
        (Guid AssignmentId) : IRequest<Result<ResponseAssignment>>;
}
