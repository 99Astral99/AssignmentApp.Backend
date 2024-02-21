using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Queries.GetAssignment
{
    public class GetAssignmentQueryHandler : IRequestHandler<GetAssignmentQuery, Result<ResponseAssignment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAssignmentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignment>> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.AssignmentId);

            if (assignment is null)
                throw new NotFoundException(nameof(Assignment), request.AssignmentId);

            return Result.Ok(_mapper.Map<ResponseAssignment>(assignment));
        }
    }
}
