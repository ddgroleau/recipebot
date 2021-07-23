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
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var testRecipeDTO = new RecipeDTO(logger);
            testRecipeDTO.NewIngredient = "sugar";

            testRecipeDTO.AddIngredient();

            Assert.Equal(testRecipeDTO.NewIngredient, testRecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddIngredient_WithInvalidString_ShouldNotValidate()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var testRecipeDTO = new RecipeDTO(logger);
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
        public void AddIngredient_WithEmptyString_ShouldNotValidate()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var testRecipeDTO = new RecipeDTO(logger);
            var validationContext = new ValidationContext(testRecipeDTO)
            {
                MemberName = "NewIngredient"
            };

            testRecipeDTO.NewIngredient = "";

            testRecipeDTO.AddIngredient();

            bool propertyIsValid = Validator.TryValidateProperty(testRecipeDTO.NewIngredient, validationContext, new List<ValidationResult>());

            Assert.False(propertyIsValid);
            Assert.Null(testRecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddInstruction_WithValidString_ShouldAddToInstructions()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var testRecipeDTO = new RecipeDTO(logger);
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
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var testRecipeDTO = new RecipeDTO(logger);
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
        public void AddInstruction_WithEmptyString_ShouldNotValidate()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var testRecipeDTO = new RecipeDTO(logger);
            var validationContext = new ValidationContext(testRecipeDTO)
            {
                MemberName = "NewInstruction"
            };

            testRecipeDTO.NewInstruction = "";

            testRecipeDTO.AddInstruction();

            bool propertyIsValid = Validator.TryValidateProperty(testRecipeDTO.NewInstruction, validationContext, new List<ValidationResult>());

            Assert.False(propertyIsValid);
            Assert.Null(testRecipeDTO.Instructions.FirstOrDefault());
        }

        [Fact]
        public void ResetRecipe_WithValidObject_ShouldResetObject()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var expectedRecipeDTO = new RecipeDTO(logger);
            var actualRecipeDTO = new RecipeDTO(logger);

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

        [Fact]
        public void ReadRecipe_WithEmptyContent_ShouldReturnSelf()
        {
            var content = new HttpResponseMessage().Content;
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            
            var recipeDTO = new RecipeDTO(logger);

            var result = recipeDTO.ReadRecipe(content).Result;

            Assert.Equal(recipeDTO, result);
        }
        [Fact]
        public void ReadRecipe_WithValidObject_ShouldBeEqual()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var recipeDTO = new RecipeDTO(logger);
            var otherRecipeDTO = new RecipeDTO(logger)
            {
                Title = "test"
            };
            var request = new HttpRequestMessage()
            {
                Content = new ObjectContent<RecipeDTO>(otherRecipeDTO, new JsonMediaTypeFormatter()),
                RequestUri = new Uri("http://www.google.com")
            };
            var message = new HttpClient().SendAsync(request);

            var result = recipeDTO.ReadRecipe(message.Result.Content).Result;

            Assert.Equal(otherRecipeDTO, result);
        }
    }
}
