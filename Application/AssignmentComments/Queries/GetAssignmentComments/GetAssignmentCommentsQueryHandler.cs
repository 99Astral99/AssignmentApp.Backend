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

namespace AssignmentApp.Application.AssignmentComments.Queries.GetAssignmentComments
{
    public class GetAssignmentCommentsQueryHandler : IRequestHandler<GetAssignmentCommentsQuery, PaginatedList<ResponseAssignmentComment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAssignmentCommentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ResponseAssignmentComment>> Handle(GetAssignmentCommentsQuery request, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == request.AssignmentId);

            if (assignment is null)
                throw new NotFoundException(nameof(Assignment), request.AssignmentId);

            return await _context.AssignmentComments
                .AsNoTracking()
                .Where(x => x.AssignmentId == request.AssignmentId)
                .ProjectTo<ResponseAssignmentComment>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
