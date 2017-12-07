using System.Data.Entity;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Contracts
{
    public static class DatabaseNames
    {
        public const string BedeGo = "Bede.Go";
    }

    public class Context<TType> : DbContext where TType : class, IIdentifiable
    {
        public Context() : base(DatabaseNames.BedeGo) { }

        public DbSet<TType> DbSet { get; set; }
    }

    public class BedeGoMigrationContext : DbContext
    {
        public BedeGoMigrationContext() : base(DatabaseNames.BedeGo) {}

        public DbSet<Game> Games { get; set; }
    }
}