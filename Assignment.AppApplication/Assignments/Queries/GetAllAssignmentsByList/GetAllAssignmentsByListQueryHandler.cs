using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Common.Mappings;
using AssignmentApp.Application.Common.Pagination;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Queries.GetAllAssignmentsByList
{
    public class GetAllAssignmentsByListQueryHandler : IRequestHandler<GetAllAssignmentsByListQuery, PaginatedList<ResponseAssignment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAssignmentsByListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ResponseAssignment>> Handle(GetAllAssignmentsByListQuery request, CancellationToken cancellationToken)
        {
            var assignmentList = await _context.AssignmentLists.FirstOrDefaultAsync(x => x.Id == request.AssignmentListId);

            if (assignmentList is null)
                throw new NotFoundException(nameof(AssignmentList), request.AssignmentListId);

            return await _context.Assignments
                .AsNoTracking()
                .Where(x => x.AssignmentListId == request.AssignmentListId)
                .OrderBy(x => x.CurrentStatus)
                .ProjectTo<ResponseAssignment>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
