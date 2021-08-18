using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeMemento : IRecipeMemento
    {
        private bool UserRecipesHaveBeenChanged = true;
        private List<RecipeDTO> UserRecipes = new();
        private HttpClient Http = new();
        private ILogger<IRecipeMemento> _logger;

        public RecipeMemento(ILogger<IRecipeMemento> logger)
        {
            _logger = logger;
        }

        public async Task<List<RecipeDTO>> GetUserRecipesAsync(string userName)
        {
            try
            {
                if (UserRecipesHaveBeenChanged)
                {
                    UserRecipes = await Http.GetFromJsonAsync<List<RecipeDTO>>($"https://localhost:4001/api/Recipe/UserRecipes/{userName}");
                    UserRecipesHaveBeenChanged = false;
                }
            }
            catch (Exception)
            {
                _logger.LogError($"RecipeMemento failed to update its UserRecipes property. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return UserRecipes;
        }

        public bool UpdateState()
        {
            UserRecipesHaveBeenChanged = true;
            return UserRecipesHaveBeenChanged;
        }
    }
}
