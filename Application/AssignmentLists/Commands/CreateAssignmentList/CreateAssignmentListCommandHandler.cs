using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.AssignmentLists.Commands.CreateAssignmentList
{
    public class CreateAssignmentListCommandHandler : IRequestHandler<CreateAssignmentListCommand, Result<ResponseAssignmentList>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAssignmentListCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ResponseAssignmentList>> Handle(CreateAssignmentListCommand request,
            CancellationToken cancellationToken)
        {
            var assingmentList = AssignmentList.Create(Guid.NewGuid(), request.Name,
                request.Description, request.UserId).Value;

            await _context.AssignmentLists.AddAsync(assingmentList);
            await _context.SaveChangesAsync(cancellationToken);

            var createdAssignmentList = await _context.AssignmentLists.FirstOrDefaultAsync(x => x.Id == assingmentList.Id);
            return Result.Ok(_mapper.Map<ResponseAssignmentList>(createdAssignmentList));
        }
    }
}
