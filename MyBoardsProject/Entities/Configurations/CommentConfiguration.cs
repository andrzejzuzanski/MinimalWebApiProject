using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBoardsProject.Entities.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.CreatedDate)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(c => c.UpdatedDate)
                .ValueGeneratedOnUpdate();
            builder.HasOne(c => c.WorkItem)
            .WithMany(wi => wi.Comments)
            .HasForeignKey(c => c.WorkItemId);
            builder.HasOne(c => c.Author)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
