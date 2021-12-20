using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.Sqlite;
using Recipebot.Server.Data;
using System.Data.Common;

namespace UnitTests.Data
{
    public class MockDbContext
    {

        public MockDbContext()
        {
            Context = new ApplicationDbContext(
                                                new DbContextOptionsBuilder<ApplicationDbContext>()
                                                   .UseSqlite(CreateInMemoryDatabase()).Options,
                                                    Options.Create(
                                                        new OperationalStoreOptions()));
            Context.Database.EnsureCreated();
        }

        public ApplicationDbContext Context { get; }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }

    }
}
