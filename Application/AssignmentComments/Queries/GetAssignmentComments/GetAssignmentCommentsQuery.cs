using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Responses;
using MediatR;

namespace AssignmentApp.Application.AssignmentComments.Queries.GetAssignmentComments
{
    public sealed class GetAssignmentCommentsQuery : PaginationQuery, IRequest<PaginatedList<ResponseAssignmentComment>>
    {
        public Guid AssignmentId { get; set; }
    }
}
