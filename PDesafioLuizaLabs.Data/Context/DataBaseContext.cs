using DesafioLuizaLabs.Domain.Entities;
using DesafioLuizaLabs.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using PDesafioLuizaLabs.Data.Extensions;

namespace DesafioLuizaLabs.Infra.Data.Context
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> option) 
            : base(option) { }

        #region "DBSets"

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            modelBuilder.ApplyGlobalConfigurations();

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
