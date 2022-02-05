using HtmlAgilityPack;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.WebScraper
{
    public class AllRecipesScraper : IRecipeScraper
    {
        public IRecipeDTO ScrapeRecipe(string URL, IRecipeDTO RecipeDTO)
        {
            if(!URL.StartsWith("https://www.allrecipes.com/recipe/"))
            {
                return RecipeDTO;
            }
            try
            {
                HtmlWeb webPage = new HtmlWeb();
                HtmlDocument page = webPage.Load(URL);

                RecipeDTO.URL = URL;

                RecipeDTO.Title = page.DocumentNode.SelectNodes("//h1[@class='headline heading-content elementFont__display']")
                                                   .FirstOrDefault()
                                                   .InnerText
                                                   .Trim();

                var summary = page.DocumentNode.SelectNodes("//div[@class='recipe-meta-item-body elementFont__subtitle']");
                                       
                RecipeDTO.Description = page.DocumentNode
                                        .SelectNodes("//p[@class='margin-0-auto']")
                                        .FirstOrDefault().InnerText.Trim();

                RecipeDTO.Ingredients = page.DocumentNode
                                        .SelectNodes("//span[@class='ingredients-item-name elementFont__body']")
                                        .Select(x => x.InnerText.Trim()).ToList();

                RecipeDTO.Instructions = RecipeDTO.Instructions = page.DocumentNode
                                        .SelectNodes("//div[@class='section-body elementFont__body--paragraphWithin elementFont__body--linkWithin']")
                                        .Descendants()
                                        .Where(x => x.Name.Equals("p"))
                                        .Select(x => x.InnerText.Trim()).ToList();

                RecipeDTO.Description += $" Prep Time: {summary.ElementAt(0).InnerText.Trim()}. "
                +$"Cook Time: {summary.ElementAt(1).InnerText.Trim()}. "
                +$"Total Time: {summary.ElementAt(2).InnerText.Trim()}. "
                +$"Servings: {summary.ElementAt(3).InnerText.Trim()}. "
                +$"Yield: {summary.ElementAt(4).InnerText.Trim()}.";

                return RecipeDTO;
            }
            catch (ArgumentNullException)
            {
                return ScrapeRecipeFromAlternateUI(URL, RecipeDTO);
            }
            catch (Exception)
            {
                return RecipeDTO;
            }
        }

        private IRecipeDTO ScrapeRecipeFromAlternateUI(string URL, IRecipeDTO RecipeDTO)
        {
            try
            {
                HtmlWeb webPage = new HtmlWeb();
                HtmlDocument page = webPage.Load(URL);

                RecipeDTO.URL = URL;

                RecipeDTO.Title = page.DocumentNode
                                      .SelectNodes("//h1[@class='recipe-summary__h1']")
                                      .FirstOrDefault().InnerHtml;

                RecipeDTO.Description = page.DocumentNode
                                      .SelectNodes("//div[@class='submitter__description']")
                                      .FirstOrDefault().InnerHtml[7..].Replace("&#34;", "").Trim();

                RecipeDTO.Ingredients = page.DocumentNode
                                      .SelectNodes("//span[@class='recipe-ingred_txt added']")
                                      .Select(x => x.InnerHtml.Trim()).ToList();

                RecipeDTO.Instructions = RecipeDTO.Instructions = page.DocumentNode
                                      .SelectNodes("//span[@class='recipe-directions__list--item']")
                                      .Select(x => x.InnerHtml.Trim())
                                      .Where(x => !String.IsNullOrEmpty(x)).ToList();

                return RecipeDTO;
            }
            catch (Exception)
            {
                return RecipeDTO;
            }
        }

        //public async Task GetHtmlContent(string url)
        //{
        //    try
        //    {
        //        HttpResponseMessage content = await _http.(url);
                
        //    }
        //    catch(Exception e)
        //    {

        //    }
        //}

    }
}
