using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Entities;

namespace InternetCoast.Model.Context
{
    public sealed class AppDbContext : BaseDbContext
    {
        public AppDbContext(UiContext uiContext)
            : base("AppDbContext", uiContext)
        {
            //  ..... Uncomment below to create the database locally when needed .....
            InitializeDatabase();
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Agency> Agency { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        private void InitializeDatabase()
        {
            Database.SetInitializer(new DatabaseInitializer());
            if (!Database.Exists())
                Database.Initialize(true);
        }

    }
}
