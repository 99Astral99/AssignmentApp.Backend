using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Assignments.Commands.MoveAssignmentToList
{
    public class MoveAssignmentToListCommandHandler : IRequestHandler<MoveAssignmentToListCommand, Result<ResponseAssignment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoveAssignmentToListCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignment>> Handle(MoveAssignmentToListCommand request, CancellationToken cancellationToken)
        {
            var assignmentToMove = await _context.Assignments.FindAsync(request.AssignmentId);

            if (assignmentToMove == null)
                throw new NotFoundException(nameof(Assignment), request.AssignmentId);

            var sourceAssignmentList = await _context.AssignmentLists.FindAsync(request.AssignmentListId);
            var destinationAssignmentList = await _context.AssignmentLists.FindAsync(request.newAssignmentListId);

            if (sourceAssignmentList == null || destinationAssignmentList == null)
                throw new NotFoundException(nameof(AssignmentList), request.AssignmentListId);

            sourceAssignmentList.RemoveAssignment(assignmentToMove.Id);

            destinationAssignmentList.AddAssignment(assignmentToMove);

            await _context.SaveChangesAsync(cancellationToken);

            var replacedAssignment = await _context.Assignments.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == assignmentToMove.Id);

            return Result.Ok(_mapper.Map<ResponseAssignment>(replacedAssignment));
        }
    }
}
