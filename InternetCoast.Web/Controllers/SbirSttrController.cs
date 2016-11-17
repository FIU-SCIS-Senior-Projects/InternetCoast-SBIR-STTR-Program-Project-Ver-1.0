using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;

namespace InternetCoast.Web.Controllers
{
    public class SbirSttrController : Controller
    {
        // GET: SbirSttr
        public ActionResult Index()
        {
            var model = new List<Fund>();

            using (var context = new AppDbContext(new UiContext()))
            {
                var funds =
                    context.Fund.Include("Agencies")
                        .Where(f => f.Sources.Any(s => s.SourceName.Equals("SBIR Program")))
                        .ToList();

                model.AddRange(funds);
            }

            return View(model);
        }
    }
}