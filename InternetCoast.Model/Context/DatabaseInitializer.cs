using System.Collections.Generic;
using System.Data.Entity;
using InternetCoast.Model.Entities;

namespace InternetCoast.Model.Context
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var agencies = new List<Agency>
            {
                new Agency{AgencyName = "Department of Defense", Acronym = "DOD"},
                new Agency{AgencyName = "Department of Health and Human Services", Acronym = "HHS"},
                new Agency{AgencyName = "National Aeronautics and Space Administration", Acronym = "NASA"},
                new Agency{AgencyName = "National Science Foundation", Acronym = "NSF"},
                new Agency{AgencyName = "Department of Energy", Acronym = "DOE"},
                new Agency{AgencyName = "United States Department of Agriculture", Acronym = "USDA"},
                new Agency{AgencyName = "Environmental Protection Agency", Acronym = "EPA"},
                new Agency{AgencyName = "Department of Commerce", Acronym = "DOC"},
                new Agency{AgencyName = "Department of Education", Acronym = "ED"},
                new Agency{AgencyName = "Department of Transportation", Acronym = "DOT"},
                new Agency{AgencyName = "Department of Homeland Security", Acronym = "DHS"},
                new Agency{AgencyName = "Department of Education", Acronym = "ED"}
            };
            context.Agency.AddRange(agencies);

            context.SaveChanges();
        }
    }
}
