using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InternetCoast.Conectors.DOD
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://www.acq.osd.mil/osbp/sbir/solicitations/sbir20163/army163.html";
            var html = new WebClient().DownloadString(url);

            var topicsHtml = GetTopicsHtml(html);

            foreach (var topic in (List<string>)topicsHtml)
            {
                var temp = new HtmlDocument();
                temp.LoadHtml(topic);

                var cointainers = temp.DocumentNode.SelectNodes("//div");

                var title = cointainers.Where(e => e.InnerText.Contains("TITLE:")).ToList();

            }
        }

        private static IEnumerable<string> GetTopicsHtml(string html)
        {
            //var charactersToReplace = new[] { @"\t", @"\n", @"\r", @"\r\n", " " };
            //html = charactersToReplace.Aggregate(html, (current, s) => current.Replace(s, ""));

            //var startIndex = html.IndexOf("<!-- top-start -->", StringComparison.Ordinal);
            //var endIndex = html.LastIndexOf("<!-- top-end -->", StringComparison.Ordinal);
            //var length = endIndex - startIndex;

            //var topicsHtml = html.Substring(startIndex, length);


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
