using Microsoft.EntityFrameworkCore;
using MyBoardsProject.Entities;

namespace MyBoardsProject.ExtensionMethods
{
    public static class EndpointsExtensions
    {
        public static WebApplication AddEndpoints(this WebApplication app)
        {
            app.MapGet("getOnHoldEpics", async (MyBoardsContext db) =>
            {
                var onHoldEpics = await db.Epics
                .Where(e => e.WorkItemStateId == db.WorkItemStates.Where(s => s.Value == "On Hold").Select(s => s.Id).First())
                .OrderBy(e => e.Priority)
                .ToListAsync();


                return Results.Ok(onHoldEpics);
            });

            app.MapGet("getWorkItemsWithRawSql", async (MyBoardsContext db) =>
            {
                var states = await db.WorkItemStates
                .FromSqlRaw(
                    @"SELECT wis.Id, wis.Value, COUNT(*)
                    FROM WorkItemStates wis
                    JOIN WorkItems wi ON wi.WorkItemStateId = wis.Id
                    GROUP BY wis.Id, wis.Value
                    HAVING Count(*) > 85;")
                .ToListAsync();

                var update = db.Database.ExecuteSqlRaw($"INSERT INTO Tags (Value)\r\nVALUES ('Test2');");
                await db.SaveChangesAsync();

                return states;
            });

            app.MapGet("getTopAuthorsView", async (MyBoardsContext db) =>
            {
                var topAuthorsView = await db.ViewTopAuthors.ToListAsync();
                return topAuthorsView;
            });

            app.MapGet("getUsersWithComments", async (MyBoardsContext db) =>
            {
                var users = await db.Users
                .Include(u => u.Comments)
                .ToListAsync();

                return users;
            });

            app.MapGet("getUsersWithAddressesWithRawSql", (MyBoardsContext db) =>
            {
                var select = db.Users
                .FromSqlRaw(
                    @"SELECT u.Id, u.Email, u.FullName,a.Country,a.City,a.Street,a.PostalCode 
                    FROM Users u 
                    JOIN Addresses a ON a.UserId = u.Id");

                return select;
            });

            app.MapGet("AllUsersView", (MyBoardsContext db) =>
            {
                var allUsers = db.ViewAllAuthorsWithAddresses
                .AsNoTracking()
                .Take(5)
                .OrderBy(u => u.FullName);
                return allUsers;
            });

            app.MapPost("updateEpicState", async (MyBoardsContext db) =>
            {
                var epic = db.Epics
                .First(e => e.Id == 1);

                var state = db.WorkItemStates.First(wis => wis.Value == "On Hold");
                epic.WorkItemStateId = state.Id;

                await db.SaveChangesAsync();

                return epic;
            });

            app.MapPost("postNewUserWithAddress", async (MyBoardsContext db) =>
            {
                var address = new Address()
                {
                    Id = Guid.Parse("b323dd7c-776a-4cf6-a92a-12df154b4a2c"),
                    City = "Krarow",
                    Country = "Poland",
                    Street = "Dluga"
                };
                var user = new User()
                {
                    Email = "testemailaaa@o2.pl",
                    FullName = "Test user",
                    Address = address
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();
                return user;
            });

            app.MapPatch("patchTagValue", async (MyBoardsContext db) =>
            {
                var tag = db.Tags
                .First(t => t.Id == 9);

                tag.Value = "Changed value";
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("deleteUserWithComments", async (MyBoardsContext db) =>
            {
                var user = db.Users
                .Include(u => u.Comments)
                .First(u => u.Id == Guid.Parse("4EBB526D-2196-41E1-CBDA-08DA10AB0E61"));

                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("rawDelete", async (MyBoardsContext db) =>
            {
                db.Database
                .ExecuteSqlRaw(
                    @"DELETE FROM Tags 
                    WHERE Value IN ('Changed value','Test2')");
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            return app;
        }
    }
}
