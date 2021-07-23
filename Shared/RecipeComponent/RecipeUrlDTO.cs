using PBC.Shared.Custom_Validation;
using PBC.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeUrlDTO : IRecipeUrlDTO
    {
        [AcceptableURL]
        public string URL { get; set; } = "";

        public void ResetURL()
        {
            URL = null;
        }

        public async Task<IRecipeDTO> PostRecipeUrl(HttpClient Http, IRecipeUrlDTO recipeUrlDTO, IRecipeDTO recipeDTO)
        {
                try
                {
                var response = await Http.PostAsJsonAsync<IRecipeUrlDTO>("/api/Recipe/RecipeURL", this);
                var result = await recipeDTO.ReadRecipe(response.Content);

                recipeDTO = result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            return recipeDTO;
        }
    }
}
