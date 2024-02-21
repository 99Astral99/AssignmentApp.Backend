using AssignmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentApp.Persistence.EntityTypeConfigurations
{
    public class AssignmentCommentConfiguration : IEntityTypeConfiguration<AssignmentComment>
    {
        public void Configure(EntityTypeBuilder<AssignmentComment> builder)
        {
            builder.Property(x => x.Message).HasColumnName("Комментарий");
        }
    }
}
