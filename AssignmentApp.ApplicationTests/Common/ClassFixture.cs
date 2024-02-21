using MediatR;

namespace AssignmentApp.ApplicationTests.Common
{
    public abstract class ClassFixture : IClassFixture<ApplicationFixture>
    {
        protected readonly IMediator Mediator;
        protected readonly Guid NotPossibleId;
        protected readonly Guid UserId;
        protected readonly string SomeString;

        protected ClassFixture(ApplicationFixture applicationFixture)
        {
            Mediator = applicationFixture.Mediator;
            NotPossibleId = new Guid("719AA9BD-B996-46C7-A59A-FE3F875A2AAF");
            SomeString = "5ED4531B-B19B-4C13-B5F3-57940926BBA6";
            UserId = new Guid("{7E0F4D7A-4A70-49F0-95F0-8E70C07447FD}");
        }
    }
}
