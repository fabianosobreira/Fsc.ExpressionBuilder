using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Data.Common;

namespace ExpressionBuilder.Test.Database
{
    [TestFixture(Category = "Database")]
    public abstract class DatabaseTest
    {
        private static readonly object _lock = new();

        protected SqliteConnection connection;
        protected DbDataContext db;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            lock (_lock)
            {
                connection = new SqliteConnection("Filename=:memory:");
                connection.Open();

                db = CreateContext(connection);
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            db.Dispose();
            connection.Dispose();
        }

        private static DbDataContext CreateContext(DbConnection connection)
                => new DbDataContext(
                    new DbContextOptionsBuilder<DbDataContext>()
                        .UseSqlite(connection)
                        .Options);
    }
}
