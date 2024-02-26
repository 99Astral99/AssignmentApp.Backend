using AssignmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Persistence.EntityTypeConfigurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Название");
            builder.Property(x => x.Description).HasColumnName("Описание");

            builder.Property(x => x.DateCreated).HasColumnName("Дата_создания");

            builder.Property(x => x.CurrentStatus).HasConversion<string>().HasColumnName("Статус");

            builder.HasMany(x => x.Comments)
                .WithOne()
                .HasForeignKey(x => x.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
