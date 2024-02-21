using AssignmentApp.Application.Interfaces;
using AssignmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Persistence
{
    public class AssignmentDbContext : DbContext, IApplicationDbContext
    {
        public AssignmentDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AssignmentDbContext).Assembly);
        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AssignmentList> AssignmentLists { get; set; }
        public DbSet<AssignmentComment> AssignmentComments { get; set; }
    }
}
