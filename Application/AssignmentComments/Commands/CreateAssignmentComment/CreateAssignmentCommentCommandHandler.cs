using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentComments.Commands.CreateAssignmentComment
{
    public class CreateAssignmentCommentCommandHandler : IRequestHandler<CreateAssignmentCommentCommand, Result<ResponseAssignmentComment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAssignmentCommentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignmentComment>> Handle(CreateAssignmentCommentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _context.Assignments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.AssignmentId);
            if (assignment is null)
                throw new NotFoundException(nameof(Assignment), request.AssignmentId);

            var assignmentComment = AssignmentComment.Create(Guid.NewGuid(), request.AssignmentId, request.Message);

            await _context.AssignmentComments.AddAsync(assignmentComment);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok(_mapper.Map<ResponseAssignmentComment>(assignmentComment));
        }
    }
}
