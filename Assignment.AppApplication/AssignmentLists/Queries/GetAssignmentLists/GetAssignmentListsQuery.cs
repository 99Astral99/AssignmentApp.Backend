using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Responses;
using MediatR;

namespace AssignmentApp.Application.AssignmentLists.Queries.GetAssignmentLists
{
    public class GetAssignmentListsQuery : PaginationQuery, IRequest<PaginatedList<ResponseAssignmentList>>
    {
        public Guid UserId { get; set; }
    }
}
