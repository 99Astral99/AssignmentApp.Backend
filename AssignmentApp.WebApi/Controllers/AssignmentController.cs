using AssignmentApp.Application.Assignments.Commands.CreateAssignment;
using AssignmentApp.Application.Assignments.Commands.DeleteAssignment;
using AssignmentApp.Application.Assignments.Commands.MoveAssignmentToList;
using AssignmentApp.Application.Assignments.Commands.UpdateAssignment;
using AssignmentApp.Application.Assignments.Commands.UpdateStatus;
using AssignmentApp.Application.Assignments.Queries.GetAllAssignments;
using AssignmentApp.Application.Assignments.Queries.GetAllAssignmentsByList;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.WebApi.Controllers
{
    //[Authorize]
    public class AssignmentController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllAssignmentsQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllByList([FromQuery] GetAllAssignmentsByListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateAssignmentCommand command)
        {
            var assignment = await Mediator.Send(command);
            return Created(nameof(Create), assignment.Value);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateAssignmentCommand command)
        {
            var updatedAssignment = await Mediator.Send(command);
            return Ok(updatedAssignment.Value);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteAssignmentCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> MoveToList([FromBody] MoveAssignmentToListCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStatus(UpdateStatusCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
