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
        public async Task<IRecipeDTO> HandleValidSubmit(ILazor lazor, IRecipeDTO recipeDTO)
        {
            try
            {
                bool recipeDTOIsValid = lazor.IsObjectValid(recipeDTO);

                if (recipeDTOIsValid)
                {
                    lazor.SetLoadingStatus(true);
                    var response = await _http.PostAsJsonAsync("/api/recipe/recipe", recipeDTO);
                    if (response.IsSuccessStatusCode)
                    {
                        lazor.SetSuccessStatus(true);
                        _logger.LogInformation($"Successfully posted recipe \"{recipeDTO.Title}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                        ResetRecipe(recipeDTO);
                        return recipeDTO;
                    }
                    _logger.LogError($"Failed to post recipe \"{recipeDTO.Title}\" to RecipeController. Server responded with {response.StatusCode}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    lazor.SetErrorMessage($"Sorry, something went wrong. Server responded with {response.StatusCode}.");
                    return recipeDTO;
                }
            }
            catch (Exception err)
            {
                lazor.SetErrorMessage($"Sorry, something went wrong. Error {lazor.ErrorMessage}.");
                _logger.LogError($"Exception occured when posting recipe \"{recipeDTO.Title}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", err.Message);
            }
            lazor.SetLoadingStatus(false);
            return recipeDTO;
        }
        public void ResetRecipe(IRecipeDTO recipeDTO)
        {
            recipeDTO.URL = null;
            recipeDTO.Title = null;
            recipeDTO.Description = null;
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();
        }

        public IRecipeDTO AddIngredient(IRecipeDTO recipeDTO)
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = "NewIngredient"
            };
            bool newIngredientIsValid = Validator.TryValidateProperty(NewIngredient, validationContext, new List<ValidationResult>());

            if (newIngredientIsValid) { recipeDTO.Ingredients.Add(NewIngredient); }

            NewIngredient = null;

            return recipeDTO;
        }
        public IRecipeDTO AddInstruction(IRecipeDTO recipeDTO)
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = "NewInstruction"
            };
            bool newInstructionIsValid = Validator.TryValidateProperty(NewInstruction, validationContext, new List<ValidationResult>());

            if (newInstructionIsValid) { recipeDTO.Instructions.Add(NewInstruction); }

            NewInstruction = null;

            return recipeDTO;
        }
    }
}


