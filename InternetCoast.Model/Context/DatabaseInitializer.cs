using System.Collections.Generic;
using System.Data.Entity;
using InternetCoast.Model.Entities;

namespace InternetCoast.Model.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var agencies = new List<Agency>
            {
                new Agency{Active = true, AgencyName = "Department of Defense", Acronym = "DOD"},
                new Agency{Active = true, AgencyName = "Department of Health and Human Services", Acronym = "HHS"},
                new Agency{Active = true, AgencyName = "National Aeronautics and Space Administration", Acronym = "NASA"},
                new Agency{Active = true, AgencyName = "National Science Foundation", Acronym = "NSF"},
                new Agency{Active = true, AgencyName = "Department of Energy", Acronym = "DOE"},
                new Agency{Active = true, AgencyName = "United States Department of Agriculture", Acronym = "USDA"},
                new Agency{Active = true, AgencyName = "Environmental Protection Agency", Acronym = "EPA"},
                new Agency{Active = true, AgencyName = "Department of Commerce", Acronym = "DOC"},
                new Agency{Active = true, AgencyName = "Department of Education", Acronym = "ED"},
                new Agency{Active = true, AgencyName = "Department of Transportation", Acronym = "DOT"},
                new Agency{Active = true, AgencyName = "Department of Homeland Security", Acronym = "DHS"},
                new Agency{Active = true, AgencyName = "Department of Education", Acronym = "ED"},
            };
            context.Agency.AddRange(agencies);

            context.SaveChanges();
        }
    }

}
