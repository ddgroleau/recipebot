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
            if(!URL.StartsWith("https://www.allrecipes.com/recipe/"))
            {
                return recipeDTO;
            }
            try
            {
                HtmlWeb webPage = new HtmlWeb();
                HtmlDocument page = webPage.Load(URL);

                recipeDTO.URL = URL;
                recipeDTO.Title = page.DocumentNode
                                        .SelectNodes("//h1[@class='headline heading-content']")
                                        .FirstOrDefault().InnerHtml.Trim();

                recipeDTO.Description = page.DocumentNode
                                        .SelectNodes("//p[@class='margin-0-auto']")
                                        .FirstOrDefault().InnerHtml.Trim();

                recipeDTO.Ingredients = page.DocumentNode
                                        .SelectNodes("//span[@class='ingredients-item-name']")
                                        .Select(x => x.InnerHtml.Trim()).ToList();

                recipeDTO.Instructions = recipeDTO.Instructions = page.DocumentNode
                                        .SelectNodes("//li[@class='subcontainer instructions-section-item']")
                                        .Descendants()
                                        .Where(x => x.Name.Equals("p"))
                                        .Select(x => x.InnerHtml.Trim()).ToList();

                var summary = page.DocumentNode
                                        .SelectNodes("//div[@class='recipe-meta-item-body']")
                                        .Select(x => x.InnerHtml.Trim());

                var summaryItems = new Dictionary<string, string>()
                {
                    { "Prep",summary.ElementAt(0) },
                    { "Cook",summary.ElementAt(1) },
                    { "Total",summary.ElementAt(2) },
                    { "Servings",summary.ElementAt(3) },
                    { "Yield",summary.ElementAt(4)}
                };

                recipeDTO.Description += $" Prep Time: {summaryItems["Prep"]}. "
                 + $"Cook Time: {summaryItems["Cook"]}. "
                 + $"Total Time: {summaryItems["Total"]}. "
                 + $"Servings: {summaryItems["Servings"]}. "
                 + $"Yield: {summaryItems["Yield"]}.";

                return recipeDTO;
            }
            catch (ArgumentNullException)
            {
                return ScrapeAlternateUI(URL, recipeDTO);
            }
            catch (Exception)
            {
                return recipeDTO;
            }
        }

        private IRecipeDTO ScrapeAlternateUI(string URL, IRecipeDTO recipeDTO)
        {
            try
            {
                HtmlWeb webPage = new HtmlWeb();
                HtmlDocument page = webPage.Load(URL);

                recipeDTO.URL = URL;
                recipeDTO.Title = page.DocumentNode
                                      .SelectNodes("//h1[@class='recipe-summary__h1']")
                                      .FirstOrDefault().InnerHtml;
                recipeDTO.Description = page.DocumentNode
                                      .SelectNodes("//div[@class='submitter__description']")
                                      .FirstOrDefault().InnerHtml[7..].Replace("&#34;", "").Trim();
                recipeDTO.Ingredients = page.DocumentNode
                                      .SelectNodes("//span[@class='recipe-ingred_txt added']")
                                      .Select(x => x.InnerHtml.Trim().Replace("&#174;", "")).ToList();
                recipeDTO.Instructions = recipeDTO.Instructions = page.DocumentNode
                                      .SelectNodes("//span[@class='recipe-directions__list--item']")
                                      .Select(x => x.InnerHtml.Trim())
                                      .Where(x => !String.IsNullOrEmpty(x)).ToList();

                return recipeDTO;
            }
            catch (Exception)
            {
                return recipeDTO;
            }
        }

    }
}
