using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Commands.UpdateAssignment
{
    public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, Result<ResponseAssignment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAssignmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignment>> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignmentList = await _context.AssignmentLists
                .Include(x => x.Assignments)
                .SingleOrDefaultAsync(x => x.Id == request.AssignmentListId);

            if (assignmentList is null)
                throw new NotFoundException(nameof(AssignmentList), request.AssignmentListId);

            var assignment = assignmentList.Assignments
                .FirstOrDefault(x => x.Id == request.AssignmentId);

            if (assignment is null)
                throw new NotFoundException(nameof(Assignment), request.AssignmentId);

            assignmentList.UpdateAssignment(request.AssignmentId, request.Name, request.Description);
            await _context.SaveChangesAsync(cancellationToken);

            var updatedAssignment = await _context.Assignments
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(a => a.Id == assignment.Id);

            return Result.Ok(_mapper.Map<ResponseAssignment>(updatedAssignment));

        }
    }
}
