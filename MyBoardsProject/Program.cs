using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyBoardsProject.Entities;
using MyBoardsProject.ExtensionMethods;
using System.Net.WebSockets;
using System.Text.Json.Serialization;

namespace MyBoardsProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //SQL Connection string configuration
            builder.Services.AddDbContext<MyBoardsContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString")));

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.    
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

            // Apply pending migrations
            var pendingMigrations = dbContext.Database.GetMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            // Initial seed with two users if database is empty
            var users = dbContext.Users.ToList();
            if (!users.Any())
            { 
                var userOne = new User()
                {
                    Email = "userOne@test.com",
                    FullName = "User One",
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Szeroka"
                    }
                };
                var userTwo = new User()
                {
                    Email = "userTwo@test.com",
                    FullName = "User Two",
                    Address = new Address()
                    {
                        City = "Gliwice",
                        Street = "Kozielska"
                    }
                };
                dbContext.Users.AddRange(userOne, userTwo);
                dbContext.SaveChanges();
            }

            app.AddEndpoints();

            app.Run();
        }
    }
}
