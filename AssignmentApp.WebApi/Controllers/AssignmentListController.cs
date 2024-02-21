using AssignmentApp.Application.AssignmentLists.Commands.CreateAssignmentList;
using AssignmentApp.Application.AssignmentLists.Commands.DeleteAssignmentlist;
using AssignmentApp.Application.AssignmentLists.Commands.UpdateAssignmentList;
using AssignmentApp.Application.AssignmentLists.Queries.GetAssignmentLists;
using AssignmentApp.Application.Assignments.Queries.GetAllAssignmentsByList;
using AssignmentApp.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.WebApi.Controllers
{
    //[Authorize]
    public class AssignmentListController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ResponseAssignmentList>> GetAll([FromQuery] GetAssignmentListsQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAssignmentList>> GetById([FromQuery] GetAllAssignmentsByListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseAssignmentList>> Create([FromBody] CreateAssignmentListCommand command)
        {
            var assignmentList = await Mediator.Send(command);
            return Created(nameof(Create), assignmentList.Value);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAssignmentList>> Update([FromBody] UpdateAssignmentListCommand command)
        {
            var updatedAssignmentList = await Mediator.Send(command);
            return Ok(updatedAssignmentList);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteAssignmentListCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
