using System.Linq;
using System.Web.Mvc;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;

namespace InternetCoast.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new AppDbContext(new UiContext()))
            {
                var sbir =
                    context.Fund.Include("Agencies")
                        .Where(f => f.Sources.Any(s => s.SourceName.Equals("SBIR Program")))
                        .ToList();
                
                ViewBag.Sbir = sbir;
            }

            return View();
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