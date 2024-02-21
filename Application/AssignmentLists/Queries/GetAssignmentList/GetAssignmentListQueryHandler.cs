using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentLists.Queries.GetAssignmentList
{
    public class GetAssignmentListQueryHandler : IRequestHandler<GetAssignmentListQuery, Result<ResponseAssignmentList>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAssignmentListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignmentList>> Handle(GetAssignmentListQuery request, CancellationToken cancellationToken)
        {
            var assignmentList = await _context.AssignmentLists
                .Include(x => x.Assignments)
                .ThenInclude(x => x.Comments).FirstOrDefaultAsync(x => x.Id == request.Id);

            if (assignmentList is null)
                throw new NotFoundException(nameof(AssignmentList), request.Id);

            return Result.Ok(_mapper.Map<ResponseAssignmentList>(assignmentList));
        }
    }
}
