using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsProject.ViewModels;

namespace MyBoardsProject.Entities.Configurations
{
    public class AllUsersWithAdderessesConfiguration : IEntityTypeConfiguration<AllUsersWithAdderesses>
    {
        public void Configure(EntityTypeBuilder<AllUsersWithAdderesses> builder)
        {
            builder.ToView("AllUsersWithAddresses");
            builder.HasNoKey();
        }
    }
}
