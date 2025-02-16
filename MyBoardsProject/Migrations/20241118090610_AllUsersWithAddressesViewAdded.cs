using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoardsProject.Migrations
{
    /// <inheritdoc />
    public partial class AllUsersWithAddressesViewAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW AllUsersWithAddresses AS 
                SELECT u.FullName, u.Email, a.Country, a.City
                FROM Users u
                JOIN Addresses a ON a.UserId = u.Id
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW AllUsersWithAddresses");
        }
    }
}
