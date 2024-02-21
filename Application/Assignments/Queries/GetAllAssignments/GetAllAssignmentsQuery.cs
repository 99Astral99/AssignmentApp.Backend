using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Responses;
using MediatR;

namespace AssignmentApp.Application.Assignments.Queries.GetAllAssignments
{
    public class GetAllAssignmentsQuery : PaginationQuery, IRequest<PaginatedList<ResponseAssignment>> { };
}
