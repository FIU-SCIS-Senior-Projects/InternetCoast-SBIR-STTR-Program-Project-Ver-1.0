using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;

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
            var model = new List<Fund>();

            using (var context = new AppDbContext(new UiContext()))
            {
                var funds =
                    context.Fund.Include("Agencies")
                        .Where(f => f.Sources.Any(s => s.SourceName.Equals("State & Federal Grants")))
                        .ToList();

                model.AddRange(funds);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UploadDocuments(IEnumerable<HttpPostedFileBase> files)
        {
            //    using (var context = new AppDbContext(new UiContext()))
            //    {
            //        var application = context.Application.Find(model.Applicant.ApplicantId);

            //        if (application == null)
            //            return RedirectToAction("Uploads", new { model.Applicant.ApplicantId });
            //    }

            //    if (files != null)
            //    {
            //        FolderGenerator.SaveAttachments(files, model.Applicant.ApplicantId);
            //    }

            //    return RedirectToAction("Uploads", new { applicationId = model.Applicant.ApplicantId });
            return new EmptyResult();
        }
    }
}