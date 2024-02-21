using AssignmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Persistence.EntityTypeConfigurations
{
    public class AssignmentListConfiguration : IEntityTypeConfiguration<AssignmentList>
    {
        public void Configure(EntityTypeBuilder<AssignmentList> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("Название");
            builder.Property(x => x.Description).HasColumnName("Описание");

            builder.HasMany(x => x.Assignments)
                .WithOne()
                .HasForeignKey(x => x.AssignmentListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
