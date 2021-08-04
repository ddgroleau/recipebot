using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PBC.Shared.Custom_Validation;
using PBC.Shared.RecipeComponent;

namespace PBC.Shared
{
    public class RecipeDTO : IRecipeDTO
    {
        public string RecipeDtoId { get; } = Guid.NewGuid().ToString();
        [AcceptableURL]
        public string URL { get; set; }
        [Required]
        public string Title { get; set; }
        [MustBeValidType]
        public string RecipeType { get; set; }
        public string Description { get; set; }
        [ListMustContainElements]
        public List<string> Ingredients { get; set; } = new List<string>();
        [ListMustContainElements]
        public List<string> Instructions { get; set; } = new List<string>();
        [StringLength(100, ErrorMessage = "New ingredient is too long.")]
        public string NewIngredient { get; set; }
        [StringLength(350, ErrorMessage = "New instruction is too long.")]
        public string NewInstruction { get; set; }
        public void AddIngredient()
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = "NewIngredient"
            };
            bool newIngredientIsValid = Validator.TryValidateProperty(NewIngredient, validationContext, new List<ValidationResult>());

            if(newIngredientIsValid) { Ingredients.Add(NewIngredient); }
        }
        public void AddInstruction()
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = "NewInstruction"
            };
            bool newInstructionIsValid = Validator.TryValidateProperty(NewInstruction, validationContext, new List<ValidationResult>());

            if (newInstructionIsValid) { Instructions.Add(NewInstruction); }
        }
        public void ResetRecipe()
        {
            URL = null;
            Title = null;
            Description = null;
            Ingredients = new List<string>();
            Instructions = new List<string>();
            NewIngredient = null;
            NewInstruction = null;
        }
    }
}
