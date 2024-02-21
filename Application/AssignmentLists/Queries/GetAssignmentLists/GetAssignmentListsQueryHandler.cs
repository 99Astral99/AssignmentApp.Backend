using AssignmentApp.Application.Common.Mappings;
using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentLists.Queries.GetAssignmentLists
{
    public class GetAssignmentListsQueryHandler : IRequestHandler<GetAssignmentListsQuery, PaginatedList<ResponseAssignmentList>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAssignmentListsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ResponseAssignmentList>> Handle(GetAssignmentListsQuery request, CancellationToken cancellationToken)
        {
            return await _context.AssignmentLists
                .Include(x => x.Assignments)
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Name)
                .ProjectTo<ResponseAssignmentList>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
