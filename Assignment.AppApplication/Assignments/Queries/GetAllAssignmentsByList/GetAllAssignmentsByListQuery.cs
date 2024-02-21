using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Responses;
using MediatR;

namespace AssignmentApp.Application.Assignments.Queries.GetAllAssignmentsByList
{
    public class GetAllAssignmentsByListQuery : PaginationQuery, IRequest<PaginatedList<ResponseAssignment>>
    {
        public Guid AssignmentListId { get; set; }
    }
}
