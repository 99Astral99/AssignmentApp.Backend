using AssignmentApp.Application.AssignmentComments.Commands.CreateAssignmentComment;
using AssignmentApp.Application.AssignmentComments.Commands.DeleteAssignmentComment;
using AssignmentApp.Application.AssignmentComments.Commands.UpdateAssignmentComment;
using AssignmentApp.Application.AssignmentComments.Queries.GetAssignmentComments;
using AssignmentApp.Application.AssignmentLists.Commands.CreateAssignmentList;
using AssignmentApp.Application.Assignments.Commands.CreateAssignment;
using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.ApplicationTests.Common;
using AssignmentApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.ApplicationTests.Services
{
    public class AssignmentCommentTests : ClassFixture
    {
        private readonly Guid _existAssignmentId;

        public AssignmentCommentTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
            var assignmentList = Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId)).Result.Value;
            var assignment = Mediator.Send(new CreateAssignmentCommand(assignmentList.Id, SomeString, SomeString)).Result.Value;
            _existAssignmentId = assignment.Id;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("here is my message")]
        public async void CmdAssignmentCommentCreate_Success(string message)
        {
            var comment = await Mediator.Send(new CreateAssignmentCommentCommand(_existAssignmentId, message));
            Assert.Equal(message, comment.Value.Comment);
        }

        [Fact]
        public async void CmdTaskCommentCreate_TaskNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new CreateAssignmentCommentCommand(NotPossibleId, SomeString)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("here is my message")]
        public async void CmdAssignmentCommentUpdate_Success(string message)
        {
            var comment = await Mediator.Send(new CreateAssignmentCommentCommand(_existAssignmentId, SomeString));

            var updatedComment = await Mediator.Send(new UpdateAssignmentCommentCommand(comment.Value.Id, message));

            Assert.Equal(message, updatedComment.Value.Comment);
        }

        [Fact]
        public async void CmdAssignmentCommentUpdate_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new UpdateAssignmentCommentCommand(NotPossibleId, SomeString)));
        }

        [Fact]
        public async void CmdAssignmentCommentDelete_Success()
        {
            var createdComment = await Mediator.Send(new CreateAssignmentCommentCommand(_existAssignmentId, SomeString));

            await Mediator.Send(new DeleteAssignmentCommentCommand(createdComment.Value.Id));

            var assignmentComments = await Mediator.Send(new GetAssignmentCommentsQuery() { AssignmentId = _existAssignmentId });

            Assert.False(assignmentComments.Results.Any(comment => comment.Id == createdComment.Value.Id));
        }

        [Fact]
        public async void CmdTaskCommentDelete_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new DeleteAssignmentCommentCommand(NotPossibleId)));
        }

        [Fact]
        public async void CmdTaskCommentGetAll_Success()
        {
            var createdComment = await Mediator.Send(new CreateAssignmentCommentCommand(_existAssignmentId, SomeString));

            var assignmentComments = await Mediator.Send(new GetAssignmentCommentsQuery() { AssignmentId = _existAssignmentId });

            Assert.True(assignmentComments.Results.Any(comment => comment.Id == createdComment.Value.Id));
        }

        [Fact]
        public async void CmdTaskCommentGetAll_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new GetAssignmentCommentsQuery() { AssignmentId = NotPossibleId }));
        }
    }
}
