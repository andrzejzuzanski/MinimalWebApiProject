using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBoardsProject.Entities.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.Property(wi => wi.Area)
                    .HasColumnType("varchar(200)");
            builder.Property(wi => wi.IterationPath)
                .HasColumnName("Iteration_Path");
            builder.Property(wi => wi.Priority)
                .HasDefaultValue(1);
            builder.HasOne(wi => wi.Author)
            .WithMany(u => u.WorkItems)
            .HasForeignKey(wi => wi.AuthorId);
            builder.HasOne(w => w.WorkItemState)
            .WithMany()
            .HasForeignKey(w => w.WorkItemStateId);

            builder.HasMany(wi => wi.Tags)
            .WithMany(t => t.WorkItems)
            .UsingEntity<WorkItemTag>(
                w => w.HasOne(wit => wit.Tag)
                .WithMany()
                .HasForeignKey(wit => wit.TagId),

                wit => wit.HasOne(wit => wit.WorkItem)
                .WithMany()
                .HasForeignKey(wit => wit.WorkItemId),

                wit =>
                {
                    wit.HasKey(x => new { x.TagId, x.WorkItemId });
                    wit.Property(x => x.PublicationDate)
                    .HasDefaultValueSql("getutcdate()");
                });
        }
    }
}
