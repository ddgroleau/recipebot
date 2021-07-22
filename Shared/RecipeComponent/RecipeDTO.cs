using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBC.Shared.RecipeComponent;

namespace PBC.Shared
{
    public class RecipeDTO : IRecipeDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Instructions { get; set; } = new List<string>();
        [StringLength(100, ErrorMessage = "New ingredient is too long.", MinimumLength = 1)]
        public string NewIngredient { get; set; }
        [StringLength(350, ErrorMessage = "New instruction is too long.", MinimumLength = 1)]
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
