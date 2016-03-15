using SQLite.CodeFirst;
using System.Data.Entity;

namespace PhoneVerificator
{
  public class DatabasebContext : DbContext
  {
    public DbSet<PhoneVerificationLogEntry> LogEntries { get; set; }

    public DatabasebContext() : base("DefaultConnection")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabasebContext>(modelBuilder);
      Database.SetInitializer(sqliteConnectionInitializer);
    }
  }

}
