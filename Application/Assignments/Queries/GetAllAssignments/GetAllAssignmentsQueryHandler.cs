using AssignmentApp.Application.Common.Mappings;
using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Queries.GetAllAssignments
{
    public class GetAllAssignmentsQueryHandler : IRequestHandler<GetAllAssignmentsQuery, PaginatedList<ResponseAssignment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAssignmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ResponseAssignment>> Handle(GetAllAssignmentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Assignments
                .AsNoTracking()
                .ProjectTo<ResponseAssignment>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
