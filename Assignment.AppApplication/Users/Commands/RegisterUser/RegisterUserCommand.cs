using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.Application.Users.Commands.RegisterUser
{
    public sealed record RegisterUserCommand([Required] string Name,
        [Required] string Email, [Required] string Password) : IRequest<Result>;
}
