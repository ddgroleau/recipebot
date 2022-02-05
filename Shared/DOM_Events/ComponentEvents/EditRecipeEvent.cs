using Microsoft.Extensions.Logging;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.DOM_Events.ComponentEvents
{
    public class EditRecipeEvent : IEditRecipeEvent
    {
        private readonly ILogger<IRecipeDTO> _logger;
        private readonly HttpClient _http;

        public EditRecipeEvent(ILogger<IRecipeDTO> logger, HttpClient http)
        {
            _logger = logger;
            _http = http;
        }
        [MaxLength(100, ErrorMessage = "New ingredient is too long.")]
        public string NewIngredient { get; set; }

        [MaxLength(350, ErrorMessage = "New instruction is too long.")]
        public string NewInstruction { get; set; }
        public async Task<IRecipeDTO> HandleValidSubmit(ILazor lazor, IRecipeDTO RecipeDTO)
        {
            try
            {
                bool RecipeDTOIsValid = lazor.IsObjectValid(RecipeDTO);

                if (RecipeDTOIsValid)
                {
                    lazor.SetLoadingStatus(true);
                    var response = await _http.PostAsJsonAsync("/api/recipe/recipe", RecipeDTO);
                    if (response.IsSuccessStatusCode)
                    {
                        lazor.SetSuccessStatus(true);
                        _logger.LogInformation($"Successfully posted recipe \"{RecipeDTO.Title}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                        ResetRecipe(RecipeDTO);
                        return RecipeDTO;
                    }
                    _logger.LogError($"Failed to post recipe \"{RecipeDTO.Title}\" to RecipeController. Server responded with {response.StatusCode}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    lazor.SetErrorMessage($"Sorry, something went wrong. Server responded with {response.StatusCode}.");
                    return RecipeDTO;
                }
            }
            catch (Exception err)
            {
                lazor.SetErrorMessage($"Sorry, something went wrong. Error {lazor.ErrorMessage}.");
                _logger.LogError($"Exception occured when posting recipe \"{RecipeDTO.Title}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", err.Message);
            }
            lazor.SetLoadingStatus(false);
            return RecipeDTO;
        }
        public void ResetRecipe(IRecipeDTO RecipeDTO)
        {
            RecipeDTO.URL = null;
            RecipeDTO.Title = null;
            RecipeDTO.Description = null;
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();
        }

        public IRecipeDTO AddIngredient(IRecipeDTO RecipeDTO)
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = "NewIngredient"
            };
            bool newIngredientIsValid = Validator.TryValidateProperty(NewIngredient, validationContext, new List<ValidationResult>());

            if (newIngredientIsValid) { RecipeDTO.Ingredients.Add(NewIngredient); }

            NewIngredient = null;

            return RecipeDTO;
        }
        public IRecipeDTO AddInstruction(IRecipeDTO RecipeDTO)
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = "NewInstruction"
            };
            bool newInstructionIsValid = Validator.TryValidateProperty(NewInstruction, validationContext, new List<ValidationResult>());

            if (newInstructionIsValid) { RecipeDTO.Instructions.Add(NewInstruction); }

            NewInstruction = null;

            return RecipeDTO;
        }
    }
}


