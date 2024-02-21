using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentLists.Commands.UpdateAssignmentList
{
    public class UpdateAssignmentListCommandHandler : IRequestHandler<UpdateAssignmentListCommand, Result<ResponseAssignmentList>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAssignmentListCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignmentList>> Handle(UpdateAssignmentListCommand request,
            CancellationToken cancellationToken)
        {
            var assignmentList = await _context.AssignmentLists
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (assignmentList is null)
                throw new NotFoundException(nameof(AssignmentList), request.Id);

            assignmentList.UpdateAssignmentListInfo(request.Name, request.Description);

            await _context.SaveChangesAsync(cancellationToken);

            var updatedAssignmentList = await _context.AssignmentLists.FirstOrDefaultAsync(x => x.Id == assignmentList.Id);
            return Result.Ok(_mapper.Map<ResponseAssignmentList>(updatedAssignmentList));
        }
    }
}
