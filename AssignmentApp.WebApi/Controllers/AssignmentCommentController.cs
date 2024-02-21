using AssignmentApp.Application.AssignmentComments.Commands.CreateAssignmentComment;
using AssignmentApp.Application.AssignmentComments.Commands.DeleteAssignmentComment;
using AssignmentApp.Application.AssignmentComments.Commands.UpdateAssignmentComment;
using AssignmentApp.Application.AssignmentComments.Queries.GetAssignmentComments;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.WebApi.Controllers
{
    //[Authorize]
    public class AssignmentCommentController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateAssignmentCommentCommand command)
        {
            var assignmentCommentId = await Mediator.Send(command);

            return Created(nameof(Create), assignmentCommentId.Value);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateAssignmentCommentCommand command)
        {
            var updatedComment = await Mediator.Send(command);
            return Ok(updatedComment.Value);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteAssignmentCommentCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAssignmentCommentsQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
