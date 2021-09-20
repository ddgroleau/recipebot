using IdentityServer4.EntityFramework.Options;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PBC.Server.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
