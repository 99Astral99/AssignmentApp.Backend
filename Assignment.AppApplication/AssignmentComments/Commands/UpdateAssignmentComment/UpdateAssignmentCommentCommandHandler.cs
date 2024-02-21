using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentComments.Commands.UpdateAssignmentComment
{
    public class UpdateAssignmentCommentCommandHandler : IRequestHandler<UpdateAssignmentCommentCommand, Result<ResponseAssignmentComment>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAssignmentCommentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignmentComment>> Handle(UpdateAssignmentCommentCommand request, CancellationToken cancellationToken)
        {
            var existAssignmentComment = await _context.AssignmentComments.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existAssignmentComment is null)
                throw new NotFoundException(nameof(AssignmentComment), request.Id);

            existAssignmentComment.UpdateCommentInfo(request.Message);
            await _context.SaveChangesAsync(cancellationToken);

            var updatedComment = await _context.AssignmentComments.FirstOrDefaultAsync(x => x.Id == existAssignmentComment.Id);
            return Result.Ok(_mapper.Map<ResponseAssignmentComment>(updatedComment));
        }
    }
}
