using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore.Migrations;
using MyBoardsProject.Entities;

#nullable disable

namespace MyBoardsProject.Migrations
{
    /// <inheritdoc />
    public partial class AdditionaWorkItemStateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "On Hold"
                );
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                column: "Value",
                value: "Rejected"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "On Hold"
                );
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Value",
                keyValue: "Rejected"
    );
        }
    }
}
