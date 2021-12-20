using HtmlAgilityPack;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.WebScraper
{
    public class AllRecipesScraper : IAllRecipesScraper
    {
        public IRecipeDTO ScrapeRecipe(string URL, IRecipeDTO recipeDTO)
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
                                        .SelectNodes("//h1[@class='headline heading-content elementFont__display']")
                                        .FirstOrDefault().InnerHtml.Trim();

                var summary = page.DocumentNode
                                        .SelectNodes("//div[@class='recipe-meta-item-body']")
                                        .Select(x => x.InnerHtml.Trim());

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


                recipeDTO.Description += $" Prep Time: {summary.ElementAt(0)}. "
                 + $"Cook Time: {summary.ElementAt(1)}. "
                 + $"Total Time: {summary.ElementAt(2)}. "
                 + $"Servings: {summary.ElementAt(3)}. "
                 + $"Yield: {summary.ElementAt(4)}.";

                return recipeDTO;
            }
            catch (ArgumentNullException)
            {
                return ScrapeRecipeFromAlternateUI(URL, recipeDTO);
            }
            catch (Exception)
            {
                return recipeDTO;
            }
        }

        private IRecipeDTO ScrapeRecipeFromAlternateUI(string URL, IRecipeDTO recipeDTO)
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
                                      .Select(x => x.InnerHtml.Trim()).ToList();

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
