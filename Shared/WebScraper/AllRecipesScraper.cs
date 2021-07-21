using HtmlAgilityPack;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.WebScraper
{
    public class AllRecipesScraper
    {
        public IRecipeDTO Scrape(string URL, IRecipeDTO recipeDTO)
        {
            HtmlWeb webPage = new HtmlWeb();
            HtmlDocument page = webPage.Load(URL);

            recipeDTO.Title = page.DocumentNode
                                    .SelectNodes("//h1[@class='headline heading-content']")
                                    .FirstOrDefault().InnerHtml;

            recipeDTO.Description = page.DocumentNode
                                    .SelectNodes("//p[@class='margin-0-auto']")
                                    .FirstOrDefault().InnerHtml;

            recipeDTO.Ingredients = page.DocumentNode
                                    .SelectNodes("//span[@class='ingredients-item-name']")
                                    .Select(x => x.InnerHtml);

            recipeDTO.Instructions = page.DocumentNode
                                    .SelectNodes("//li[@class='subcontainer instructions-section-item']")
                                    .Descendants()
                                    .Where(x => x.Name.Equals("p"))
                                    .Select(x => x.InnerHtml);

            var summary = page.DocumentNode
                                    .SelectNodes("//div[@class='recipe-meta-item-body']")
                                    .Select(x => x.InnerHtml);

            var summaryItems = new Dictionary<string, string>()
                {
                    { "Prep",summary.ElementAt(0) },
                    { "Cook",summary.ElementAt(1) },
                    { "Total",summary.ElementAt(2) },
                    { "Servings",summary.ElementAt(3) },
                    { "Yield",summary.ElementAt(4)}
                };

            recipeDTO.Description += $"Prep Time: {summaryItems["Prep"]}. "
             + $"Cook Time: {summaryItems["Cook"]}. "
             + $"Total Time: {summaryItems["Total"]}. "
             + $"Servings: {summaryItems["Servings"]}. "
             + $"Yield: {summaryItems["Yield"]}.";

            return recipeDTO;
        }

    }
}
