using PBC.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeBuilder _recipeBuilder;
        private readonly IRepository<IRecipeEntity> _recipeRepository;
        public RecipeService(IRecipeBuilder recipeBuilder, IRepository<IRecipeEntity> recipeRepository)
        {
            _recipeBuilder = recipeBuilder;
            _recipeRepository = recipeRepository;
        }

        public IRecipeModel CreateRecipe(IRecipeDTO recipeDTO)
        {
            IRecipeModel recipeModel;

            if (RecipeIsValid(recipeDTO))
            {
                recipeModel = _recipeBuilder.Build(recipeDTO);
                SaveRecipe(recipeModel);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return recipeModel;
        }

        private IRecipeModel SaveRecipe(IRecipeModel recipeModel)
        {
            try
            {
                _recipeRepository.InsertOne(recipeModel);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to Add Recipe to Database.",e);
            }
            return recipeModel;
        }

        private bool RecipeIsValid(IRecipeDTO recipeDTO)
        {
            List<bool> results = new List<bool>();
            var urlContext = new ValidationContext(recipeDTO)
            {
                MemberName = "URL"
            };
            var titleContext = new ValidationContext(recipeDTO)
            {
                MemberName = "Title"
            };
            var typeContext = new ValidationContext(recipeDTO)
            {
                MemberName = "RecipeType"
            };
            var ingredientsContext = new ValidationContext(recipeDTO)
            {
                MemberName = "Ingredients"
            };
            var instructionsContext = new ValidationContext(recipeDTO)
            {
                MemberName = "Instructions"
            };
            
            results.Add(Validator.TryValidateProperty(recipeDTO.URL, urlContext, new List<ValidationResult>()));
            results.Add(Validator.TryValidateProperty(recipeDTO.Title, titleContext, new List<ValidationResult>()));
            results.Add(Validator.TryValidateProperty(recipeDTO.RecipeType, typeContext, new List<ValidationResult>()));
            results.Add(Validator.TryValidateProperty(recipeDTO.Ingredients, ingredientsContext, new List<ValidationResult>()));
            results.Add(Validator.TryValidateProperty(recipeDTO.Instructions, instructionsContext, new List<ValidationResult>()));

            return !results.Contains(false);
        }
    }
}
