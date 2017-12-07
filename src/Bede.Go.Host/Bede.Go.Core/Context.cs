using System.Data.Entity;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Core
{
    public class Context<TType> : DbContext where TType : class, IIdentifiable
    {
        public DbSet<TType> DbSet { get; set; }
    }
}