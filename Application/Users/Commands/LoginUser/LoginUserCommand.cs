using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.Application.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand([Required] string Name,
            [Required] string Email, [Required] string Password) : IRequest<Result<string>>;
}
