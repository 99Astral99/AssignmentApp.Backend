using AssignmentApp.Application.AssignmentLists.Commands.CreateAssignmentList;
using AssignmentApp.Application.AssignmentLists.Commands.DeleteAssignmentlist;
using AssignmentApp.Application.AssignmentLists.Commands.UpdateAssignmentList;
using AssignmentApp.Application.AssignmentLists.Queries.GetAssignmentList;
using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.ApplicationTests.Common;

namespace AssignmentApp.ApplicationTests.Services
{
    public class AssignmentListTests : ClassFixture
    {
        public AssignmentListTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }

        [Theory]
        [InlineData("name", "description")]
        public async void CmdAssignmentListCreate_Success(string name, string description)
        {
            var responseAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(name, description, UserId));

            Assert.Equal(name, responseAssignmentList.Value.Name);
            Assert.Equal(description, responseAssignmentList.Value.Description);
        }

        [Theory]
        [InlineData("name", "description")]
        public async void CmdAssignmentListUpdate_Success(string name, string description)
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(name, description, UserId));

            var responseAssignmentList = await Mediator.Send(new UpdateAssignmentListCommand(createdAssignmentList.Value.Id, name, description));

            Assert.Equal(name, responseAssignmentList.Value.Name);
            Assert.Equal(description, responseAssignmentList.Value.Description);
        }

        [Fact]
        public async void CmdAssignmentListUpdate_ThrowsNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new UpdateAssignmentListCommand(NotPossibleId, SomeString, SomeString)));
        }

        [Fact]
        public async void CmdAssignmentListDelete_Success()
        {
            var sourceAssignmentListToDelete = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var destinatedAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));

            await Mediator.Send(new DeleteAssignmentListCommand(sourceAssignmentListToDelete.Value.Id, destinatedAssignmentList.Value.Id));

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new GetAssignmentListQuery(sourceAssignmentListToDelete.Value.Id)));
        }

        [Fact]
        public async void CmdAssignmentListDelete_ThrowsNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new DeleteAssignmentListCommand(NotPossibleId, NotPossibleId)));
        }
    }
}
