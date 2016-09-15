using System.Data.Entity;

namespace InternetCoast.Model.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            //var roles = new List<Role>
            //{
            //    new Role{Active = true, RoleName = "Reviewer"},
            //    new Role{Active = true, RoleName = "Admin"},
            //    new Role{Active = true, RoleName = "Applicant"}
            //};
            //context.Role.AddRange(roles);

            //context.save
        }
    }

}
