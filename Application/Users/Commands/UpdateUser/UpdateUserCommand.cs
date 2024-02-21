using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.Application.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand([Required] string Name,
        [Required] string Email) : IRequest<Result>;
}
