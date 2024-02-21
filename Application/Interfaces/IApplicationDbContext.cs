using AssignmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AssignmentList> AssignmentLists { get; set; }
        public DbSet<AssignmentComment> AssignmentComments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
