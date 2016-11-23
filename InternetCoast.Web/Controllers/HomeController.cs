using System.Linq;
using System.Web.Mvc;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Web.Models.ViewModels.HomeViewModels;

namespace InternetCoast.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new FundsListViewModel();

            using (var context = new AppDbContext(new UiContext()))
            {
                var funds =
                    context.Fund.Include("Agencies")
                        .ToList();

                model.SbirSttr.AddRange(funds.Where(f => f.Sources.Any(s => s.SourceName.Equals("SBIR Program"))));
                model.Grants.AddRange(funds.Where(f => f.Sources.Any(s => s.SourceName.Equals("State & Federal Grants"))));
                model.CompetitivePrograms.AddRange(funds.Where(f => f.Sources.Any(s => s.SourceName.Equals("Competitive Program"))));
            }

            return View(model);
        }

        public ActionResult BusinessOpportunities()
        {
            var model = new FundsListViewModel();

            using (var context = new AppDbContext(new UiContext()))
            {
                var funds =
                    context.Fund.Include("Agencies")
                        .ToList();

                model.SbirSttr.AddRange(funds.Where(f => f.Sources.Any(s => s.SourceName.Equals("SBIR Program"))));
                model.Grants.AddRange(funds.Where(f => f.Sources.Any(s => s.SourceName.Equals("State & Federal Grants"))));
                model.CompetitivePrograms.AddRange(funds.Where(f => f.Sources.Any(s => s.SourceName.Equals("Competitive Program"))));
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}