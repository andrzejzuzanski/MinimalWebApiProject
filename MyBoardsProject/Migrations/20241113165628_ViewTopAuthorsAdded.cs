﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoardsProject.Migrations
{
    /// <inheritdoc />
    public partial class ViewTopAuthorsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW View_TopAuthors AS 
                SELECT TOP 5 u.FullName, Count(*) AS WorkItemsCreated
                FROM Users u 
                JOIN WorkItems wi ON wi.AuthorId = u.Id
                GROUP BY u.Id, u.FullName
                ORDER BY WorkItemsCreated DESC;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP VIEW View_TopAuthors
            ");
        }
    }
}
