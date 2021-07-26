using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    
    public class RecipeDTOTests
    {
        [Fact]
        public void AddIngredient_WithValidString_ShouldAddToIngredients()
        {
            var testRecipeDTO = new RecipeDTO();
            testRecipeDTO.NewIngredient = "sugar";

            testRecipeDTO.AddIngredient();

            Assert.Equal(testRecipeDTO.NewIngredient, testRecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddIngredient_WithInvalidString_ShouldNotValidate()
        {
            var testRecipeDTO = new RecipeDTO();
            var validationContext = new ValidationContext(testRecipeDTO)
            {
                MemberName = "NewIngredient"
            };

            testRecipeDTO.NewIngredient = "The core idea in object-oriented programming is to " +
                "divide programs into smaller pieces and make each piece responsible for managing " +
                "its own state.";

            testRecipeDTO.AddIngredient();

            bool propertyIsValid = Validator.TryValidateProperty(testRecipeDTO.NewIngredient, validationContext, new List<ValidationResult>());

            Assert.False(propertyIsValid);
            Assert.Null(testRecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddIngredient_WithEmptyString_ShouldValidate()
        {
            var testRecipeDTO = new RecipeDTO();
            var validationContext = new ValidationContext(testRecipeDTO)
            {
                MemberName = "NewIngredient"
            };

            testRecipeDTO.NewIngredient = null;

            testRecipeDTO.AddIngredient();

            bool propertyIsValid = Validator.TryValidateProperty(testRecipeDTO.NewIngredient, validationContext, new List<ValidationResult>());

            Assert.True(propertyIsValid);
            Assert.Null(testRecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddInstruction_WithValidString_ShouldAddToInstructions()
        {
            var testRecipeDTO = new RecipeDTO();
            testRecipeDTO.NewInstruction = "Different pieces of such a program interact with each other through interfaces," +
                                           " limited sets of functions or bindings that provide useful functionality " +
                                           "at a more abstract level, hiding their precise implementation. Such program " +
                                           "pieces are modeled using objects. Their interface consists of a specific set of " +
                                           "methods and properties.";

            testRecipeDTO.AddIngredient();

            Assert.Equal(testRecipeDTO.NewIngredient, testRecipeDTO.Ingredients.FirstOrDefault());
        }


        [Fact]
        public void AddInstruction_WithInvalidString_ShouldNotValidate()
        {
            var testRecipeDTO = new RecipeDTO();
            var validationContext = new ValidationContext(testRecipeDTO)
            {
                MemberName = "NewInstruction"
            };

            testRecipeDTO.NewInstruction = "When called, that method should return an object that provides a second interface," +
                                          " iterator. This is the actual thing that iterates. It has a next method that returns" +
                                          " the next result. That result should be an object with a value property that provides" +
                                          " the next value, if there is one, and a done property, which should be true when there" +
                                          " are no more results and false otherwise.";

            testRecipeDTO.AddInstruction();

            bool propertyIsValid = Validator.TryValidateProperty(testRecipeDTO.NewInstruction, validationContext, new List<ValidationResult>());

            Assert.False(propertyIsValid);
            Assert.Null(testRecipeDTO.Instructions.FirstOrDefault());
        }

        [Fact]
        public void AddInstruction_WithEmptyString_ShouldValidate()
        {
            var testRecipeDTO = new RecipeDTO();
            var validationContext = new ValidationContext(testRecipeDTO)
            {
                MemberName = "NewInstruction"
            };

            testRecipeDTO.NewInstruction = null;

            testRecipeDTO.AddInstruction();

            bool propertyIsValid = Validator.TryValidateProperty(testRecipeDTO.NewInstruction, validationContext, new List<ValidationResult>());

            Assert.True(propertyIsValid);
            Assert.Null(testRecipeDTO.Instructions.FirstOrDefault());
        }

        [Fact]
        public void ResetRecipe_WithValidObject_ShouldResetObject()
        {
            var expectedRecipeDTO = new RecipeDTO();
            var actualRecipeDTO = new RecipeDTO();

            actualRecipeDTO.URL = "https://testrecipe.com";
            actualRecipeDTO.Title = "Test Title";
            actualRecipeDTO.Description = "This is a test description.";
            actualRecipeDTO.Ingredients.Add("First Ingredient");
            actualRecipeDTO.Instructions.Add("First Instruction");
            actualRecipeDTO.NewIngredient = "Second Ingredient";
            actualRecipeDTO.NewInstruction = "Second Instruction";

            actualRecipeDTO.ResetRecipe();

            Assert.Equal(expectedRecipeDTO.URL, actualRecipeDTO.URL);
            Assert.Equal(expectedRecipeDTO.Title, actualRecipeDTO.Title);
            Assert.Equal(expectedRecipeDTO.Description, actualRecipeDTO.Description);
            Assert.Equal(expectedRecipeDTO.Ingredients, actualRecipeDTO.Ingredients);
            Assert.Equal(expectedRecipeDTO.Instructions, actualRecipeDTO.Instructions);
            Assert.Equal(expectedRecipeDTO.NewIngredient, actualRecipeDTO.NewIngredient);
            Assert.Equal(expectedRecipeDTO.NewInstruction, actualRecipeDTO.NewInstruction);
        }
    }
}
