using Estudo.TRIMANIA.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Estudo.TRIMANIA.Infrastructure.Database
{
    public class TrimaniaContext : DbContext
    {
        public TrimaniaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
