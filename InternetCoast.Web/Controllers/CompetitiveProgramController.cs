using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;
using InternetCoast.Web.Models.ViewModels.CompetitiveProgramsViewModels;

namespace InternetCoast.Web.Controllers
{
    public class CompetitiveProgramController : Controller
    {
        // GET: CompetitiveProgram
        public ActionResult Index()
        {
            var model = new HomeViewModel {NewFund = new Fund()};

            using (var context = new AppDbContext(new UiContext()))
            {
                var funds =
                    context.Fund.Include("Agencies")
                        .Where(f => f.Sources.Any(s => s.SourceName.Equals("Competitive Program")))
                        .ToList();

                model.Funds.AddRange(funds);
            }

            return View(model);
        }

        public ActionResult AddCompetitiveProgram()
        {
            var fund = new Fund();
            return View(fund);
        }

        [HttpPost]
        public ActionResult AddCompetitiveProgram(Fund newFund)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AppDbContext(new UiContext()))
                {
                    var source = context.Source.Single(s => s.SourceName == "Competitive Program");
                    newFund.Sources = new List<Source> {source};
                    context.Fund.Add(newFund);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                       ModelState.AddModelError("",e.Message);
                    }
                    
                }
            }

            return RedirectToAction("Index");
        }
    }
}