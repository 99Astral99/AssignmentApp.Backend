using AssignmentApp.Application.Interfaces;
using AssignmentApp.Application;
using AssignmentApp.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmentApp.ApplicationTests.Common
{
    public class ApplicationFixture
    {
        public ApplicationFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<AssignmentDbContext>(
                opt => opt.UseInMemoryDatabase("TestDb"));

            serviceCollection.AddScoped<IApplicationDbContext>(provider =>
                provider.GetService<AssignmentDbContext>()!);

            serviceCollection.AddApplication();

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _serviceProvider.GetRequiredService<AssignmentDbContext>();
        }

        private readonly ServiceProvider _serviceProvider;
        public IMediator Mediator => _serviceProvider.GetRequiredService<IMediator>();
    }
}
