using AssignmentApp.Application.AssignmentLists.Commands.CreateAssignmentList;
using AssignmentApp.Application.Assignments.Commands.CreateAssignment;
using AssignmentApp.Application.Assignments.Commands.DeleteAssignment;
using AssignmentApp.Application.Assignments.Commands.MoveAssignmentToList;
using AssignmentApp.Application.Assignments.Commands.UpdateAssignment;
using AssignmentApp.Application.Assignments.Queries.GetAllAssignments;
using AssignmentApp.Application.Assignments.Queries.GetAllAssignmentsByList;
using AssignmentApp.Application.Assignments.Queries.GetAssignment;
using AssignmentApp.Application.Common.Exceptions;
using AssignmentApp.ApplicationTests.Common;

namespace AssignmentApp.ApplicationTests.Services
{
    public class AssignmentTests : ClassFixture
    {
        public AssignmentTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }

        #region Commands

        [Theory]
        [InlineData("name", "description")]
        public async void CmdAssignmentCreate_Success(string name, string description)
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));

            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, name, description));

            Assert.Equal(name, createdAssignment.Value.Name);
            Assert.Equal(description, createdAssignment.Value.Description);
        }

        [Theory]
        [InlineData("name", "description")]
        public async void CmdAssignmentUpdate_Success(string name, string desc)
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, SomeString, SomeString));

            var updatedString = await Mediator.Send(new UpdateAssignmentCommand(createdAssignment.Value.Id, createdAssignmentList.Value.Id, name, desc));

            Assert.Equal(name, updatedString.Value.Name);
            Assert.Equal(desc, updatedString.Value.Description);
        }

        [Fact]
        public async void CmdAssignmentUpdate_AssignmentList_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                    await Mediator.Send(new UpdateAssignmentCommand(NotPossibleId, NotPossibleId, SomeString, SomeString)));
        }

        [Fact]
        public async void CmdAssignmentUpdate_Assignment_NotFound_()
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, SomeString, SomeString));
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                    await Mediator.Send(new UpdateAssignmentCommand(NotPossibleId, createdAssignmentList.Value.Id, SomeString, SomeString)));
        }

        [Fact]
        public async void CmdAssignmentCreate_AssignmentList_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new CreateAssignmentCommand(NotPossibleId, SomeString, SomeString)));
        }

        [Fact]
        public async void CmdAssignmentMoveToAnotherList_NotFound()
        {
            var sourceAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var destinatedAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(sourceAssignmentList.Value.Id, SomeString, SomeString));


            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new MoveAssignmentToListCommand(NotPossibleId, sourceAssignmentList.Value.Id, destinatedAssignmentList.Value.Id)));
        }

        [Fact]
        public async void CmdAssignmentMoveToAnotherList_AssignmentList_NotFound()
        {
            var sourceAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var destinatedAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(sourceAssignmentList.Value.Id, SomeString, SomeString));


            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new MoveAssignmentToListCommand(createdAssignment.Value.Id, NotPossibleId, NotPossibleId)));
        }

        [Fact]
        public async void CmdAssignmentMoveToAnotherList_Success()
        {
            var sourceAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var destinatedAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(sourceAssignmentList.Value.Id, SomeString, SomeString));

            var replacedAssignment = await Mediator.Send(new MoveAssignmentToListCommand(createdAssignment.Value.Id, sourceAssignmentList.Value.Id, destinatedAssignmentList.Value.Id));
            Assert.Equal(destinatedAssignmentList.Value.Id, replacedAssignment.Value.AssignmentListId);
        }

        [Fact]
        public async void CmdAssignmentDelete_Success()
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, SomeString, SomeString));

            await Mediator.Send(new DeleteAssignmentCommand(createdAssignment.Value.Id, createdAssignmentList.Value.Id));

            Assert.True(true);
        }

        [Fact]
        public async void CmdAssignmentDelete_Assignment_NotFound()
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new DeleteAssignmentCommand(NotPossibleId, createdAssignmentList.Value.Id)));
        }

        [Fact]
        public async void CmdAssignmentDelete_AssignmentList_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new DeleteAssignmentCommand(NotPossibleId, NotPossibleId)));
        }

        #endregion

        #region Queries

        [Theory]
        [InlineData("name1", "description1")]
        [InlineData("name2", "description2")]
        public async void QueryAssignmentGet_Success(string name, string desc)
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            var createdAssignment = await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, name, desc));

            var responseAssignment = await Mediator.Send(new GetAssignmentQuery(createdAssignment.Value.Id));

            Assert.Equal(name, responseAssignment.Value.Name);
            Assert.Equal(desc, responseAssignment.Value.Description);
        }

        [Fact]
        public async void QueryAssignmentGet_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new GetAssignmentQuery(NotPossibleId)));
        }

        [Fact]
        public async void QueryAssignmentGetAll_Success()
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, SomeString, SomeString));

            var responseAssignments = await Mediator.Send(new GetAllAssignmentsQuery());

            Assert.True(responseAssignments.Results.Any());
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public async void QueryAssignmentGetAllByList_Success(int count)
        {
            var createdAssignmentList = await Mediator.Send(new CreateAssignmentListCommand(SomeString, SomeString, UserId));
            for (var i = 0; i < count; i++)
                await Mediator.Send(new CreateAssignmentCommand(createdAssignmentList.Value.Id, SomeString, SomeString));

            var responseAssignments = await Mediator.Send(new GetAllAssignmentsByListQuery() { AssignmentListId = createdAssignmentList.Value.Id });

            Assert.Equal(count, responseAssignments.Results.Count());
        }

        [Fact]
        public async void QueryAssignmentGetAllByList_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await Mediator.Send(new GetAllAssignmentsByListQuery() { AssignmentListId = NotPossibleId }));
        }
        #endregion
    }
}
