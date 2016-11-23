using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;
using InternetCoast.Web.Models.ViewModels.GrantsViewModels;
using LumenWorks.Framework.IO.Csv;

namespace InternetCoast.Web.Controllers
{
    public class GrantsController : Controller
    {
        // GET: Grants
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Uploads()
        {
            var model = new HomeViewModel {Funds = new List<Fund>()};

            using (var context = new AppDbContext(new UiContext()))
            {
                var funds =
                    context.Fund.Include("Agencies")
                        .Where(f => f.Sources.Any(s => s.SourceName.Equals("State & Federal Grants")))
                        .ToList();

                model.Funds.AddRange(funds);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UploadDocuments(Fund fund, IEnumerable<HttpPostedFileBase> files)
        {
            var list = files.ToList();

            for (var i = 0; i < list.Count(); i++)
            {
                var file = list[i];
                var stream = file.InputStream;

                var myObjectMap = new Dictionary<string, int>();
                var funds = new List<Fund>();

                using (var csv = new CsvReader(new StreamReader(stream), true))
                {
                    var fieldCount = csv.FieldCount;
                    var headers = csv.GetFieldHeaders();

                    for (var j = 0; j < fieldCount; j++)
                    {
                        myObjectMap[headers[j]] = j; // track the index of each column name
                    }

                    using (var context = new AppDbContext(new UiContext()))
                    {
                        var agencies = context.Agency.ToList();
                        var existingFunds = context.Fund.Select(f => f.Solicitation).ToList();
                        var source = context.Source.Single(s => s.SourceName.Equals("State & Federal Grants"));

                        using (var dbTransaction = context.Database.BeginTransaction())
                        {
                            while (csv.ReadNextRecord())
                            {
                                if (
                                    csv[myObjectMap["OPPORTUNITY NUMBER"]].IndexOf("=HYPERLINK",
                                        StringComparison.Ordinal) <
                                    0)
                                    continue;

                                // EXAMPLE OF VALUE:  
                                //  =HYPERLINK("http://www.grants.gov/view-opportunity.html?oppId=289883","DE-FOA-0001630")
                                var clean =
                                    csv[myObjectMap["OPPORTUNITY NUMBER"]].Replace("=HYPERLINK(", string.Empty)
                                        .Replace(")", string.Empty)
                                        .Replace("\"", string.Empty).Split(new[] {','});

                                if (existingFunds.Contains(clean.Last()))
                                    continue;

                                var newFund = new Fund
                                {
                                    Url = clean.First(),
                                    Solicitation = clean.Last(),
                                    FundTitle = csv[myObjectMap["OPPORTUNITY TITLE"]],
                                    Agencies = new List<Agency>(),
                                    FundTopic = string.IsNullOrEmpty(fund.FundTopic) ? "NA" : fund.FundTopic,
                                    Sources = new List<Source> {source}
                                };

                                if (!string.IsNullOrEmpty(csv[myObjectMap["CLOSE DATE"]]))
                                {
                                    newFund.DeadLine = Convert.ToDateTime(csv[myObjectMap["CLOSE DATE"]]);
                                }

                                newFund.Awards = csv[myObjectMap["ESTIMATED FUNDING"]];

                                var fundAgency =
                                    agencies.SingleOrDefault(a => a.AgencyName == csv[myObjectMap["AGENCY NAME"]]);

                                if (fundAgency != null)
                                {
                                    newFund.Agencies.Add(fundAgency);
                                }
                                else
                                {
                                    var newAgency = new Agency
                                    {
                                        AgencyName = csv[myObjectMap["AGENCY NAME"]],
                                        Acronym = csv[myObjectMap["AGENCY CODE"]]
                                    };

                                    context.Agency.Add(newAgency);
                                    context.SaveChanges();

                                    agencies.Add(newAgency);
                                    newFund.Agencies.Add(newAgency);
                                }

                                funds.Add(newFund);
                            }

                            context.Fund.AddRange(funds);
                            try
                            {
                                context.SaveChanges();
                                dbTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                dbTransaction.Rollback();
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Uploads");
        }
    }
}