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
        public DbSet<Fund> Fund { get; set; }
        public DbSet<Source> Source { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fund>()
                .HasMany(f => f.Sources)
                .WithMany(s => s.Funds)
                .Map(cs =>
                {
                    cs.MapLeftKey("FundId");
                    cs.MapRightKey("SourceId");
                    cs.ToTable("FundSource");
                });

            modelBuilder.Entity<Fund>()
                .HasMany(f => f.Agencies)
                .WithMany(a => a.Funds)
                .Map(cs =>
                {
                    cs.MapLeftKey("FundId");
                    cs.MapRightKey("AgencyId");
                    cs.ToTable("FundAgency");
                });
        }

        private void InitializeDatabase()
        {
            Database.SetInitializer(new DatabaseInitializer());
            if (!Database.Exists())
                Database.Initialize(true);
        }

    }
}
