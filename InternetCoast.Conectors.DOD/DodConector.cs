using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using InternetCoast.DAL.Repositories;
using InternetCoast.Infrastructure.Data.EF.Context;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;

namespace InternetCoast.Conectors.DOD
{
    internal class DodConector
    {
        static void Main(string[] args)
        {
            const string url = "http://www.acq.osd.mil/osbp/sbir/solicitations/sbir20163/army163.html";
            var html = new WebClient().DownloadString(url);

            var topicsHtml = GetTopicsHtml(html);

            var funds = new List<Fund>();

            foreach (var topic in (List<string>)topicsHtml)
            {
                var temp = new HtmlDocument();
                temp.LoadHtml(topic);

                var divContainers = temp.DocumentNode.SelectNodes("//div");
                // remove main div
                divContainers.RemoveAt(0);

                var solicitation = divContainers.First().InnerText;

                var titleContainter = divContainers.SingleOrDefault(e => e.InnerText.Contains("TITLE:"));

                if (titleContainter == null) continue;

                var title = titleContainter.InnerText.Replace("TITLE: ", "");

                var pContainers = temp.DocumentNode.SelectNodes("//p");

                var topicArea = pContainers.First().InnerText.Replace("TECHNOLOGY AREA(S): ", "");

                var remarks = pContainers.Single(e => e.InnerText.Contains("KEYWORDS:"))
                    .InnerText.Replace("KEYWORDS: ", "");

                var fund = new Fund
                {
                    Solicitation = solicitation,
                    FundTitle = title,
                    FundTopic = topicArea,
                    Remarks = remarks,
                    Url = url
                };

                funds.Add(fund);
            }

            var repository = new FundRepository(new AppDbContext(new UiContext()));

            repository.AddList(funds);
        }

        private static IEnumerable<string> GetTopicsHtml(string html)
        {
            var subs = html.Split(new[] { "<!-- top-start -->" }, StringSplitOptions.RemoveEmptyEntries);

            var topics = (from t
                          in subs
                          where t.IndexOf("<!-- top-end -->", StringComparison.Ordinal) > -1
                          select t.Split(new[] { "<!-- top-end -->" }, StringSplitOptions.RemoveEmptyEntries)
                          into topic
                          select topic[0])
                    .ToList();

            return topics;
        }
    }
}
