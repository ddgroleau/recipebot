using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.WebScraper
{
    public class AllRecipesScraper
    {
        public void Scrape(string URL)
        {
            HtmlWeb webPage = new HtmlWeb();
            HtmlDocument page = webPage.Load(URL);

            var title = page.DocumentNode
                                    .SelectNodes("//h1[@class='headline heading-content']")
                                    .FirstOrDefault().InnerHtml;

            var description = page.DocumentNode
                                    .SelectNodes("//p[@class='margin-0-auto']")
                                    .FirstOrDefault().InnerHtml;

            var ingredients = page.DocumentNode
                                    .SelectNodes("//span[@class='ingredients-item-name']")
                                    .Select(x => x.InnerHtml);

            var instructions = page.DocumentNode
                                    .SelectNodes("//li[@class='subcontainer instructions-section-item']")
                                    .Descendants()
                                    .Where(x => x.Name.Equals("p"))
                                    .Select(x => x.InnerHtml);

            var summary = page.DocumentNode
                                    .SelectNodes("//div[@class='recipe-meta-item-body']")
                                    .Select(x => x.InnerHtml);

            var Summary = new Dictionary<string, string>()
                {
                    { "Prep Time",summary.ElementAt(0) },
                    { "Cook Time",summary.ElementAt(1) },
                    { "Total Time",summary.ElementAt(2) },
                    { "Servings",summary.ElementAt(3) },
                    { "Yield",summary.ElementAt(4)}
                };
        }

    }
}
