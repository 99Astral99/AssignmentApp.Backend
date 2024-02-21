using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, Result<ResponseAssignment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAssignmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignment>> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignmentList = await _context
                .AssignmentLists
                .SingleOrDefaultAsync(x => x.Id == request.AssignmentListId, cancellationToken);

            if (assignmentList is null)
                throw new NotFoundException(nameof(AssignmentList), request.AssignmentListId);

            var assignment = Assignment.Create(Guid.NewGuid(), request.Name, request.Description, request.AssignmentListId).Value;
            await _context.Assignments.AddAsync(assignment);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok(_mapper.Map<ResponseAssignment>(assignment));
        }

    }
}
