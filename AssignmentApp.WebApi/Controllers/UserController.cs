using AssignmentApp.Application.Users.Commands.LoginUser;
using AssignmentApp.Application.Users.Commands.RegisterUser;
using AssignmentApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.WebApi.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(request.Name, request.Email, request.Password);

            await Mediator.Send(command);

            return Results.Ok();
        }

        [HttpPost]
        public async Task<IResult> Login([FromBody] LoginUserRequest request)
        {
            var command = new LoginUserCommand(request.Name, request.Email, request.Password);

            var token = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("secretCookie", token.Value);

            return Results.Ok();
        }
    }
}
