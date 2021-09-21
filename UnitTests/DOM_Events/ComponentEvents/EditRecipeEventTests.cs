using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.DOM_Events;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.Lazor;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static PBC.Client.Components.EditRecipeModal;

namespace UnitTests.DOM_Events.ComponentEvents
{
    public class RecipeEventTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        ILogger<IRecipeDTO> Logger;
        ILazor Lazor;
        HttpClient Http;
        IEditRecipeEvent RecipeEvent;

        public RecipeEventTests()
        {
            RecipeDTO = new RecipeDTO();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Lazor = new Lazor();
            Http = new HttpClient();
            RecipeEvent = new EditRecipeEvent(Logger, Http);
        }
        public void Dispose()
        {
            Http.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async void HandleValidSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var actual = await RecipeEvent.HandleValidSubmit(Lazor, RecipeDTO);

            Assert.Equal(RecipeDTO, actual);
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

            RecipeEvent.ResetRecipe(actualRecipeDTO);

            Assert.Equal(expectedRecipeDTO.URL, actualRecipeDTO.URL);
            Assert.Equal(expectedRecipeDTO.Title, actualRecipeDTO.Title);
            Assert.Equal(expectedRecipeDTO.Description, actualRecipeDTO.Description);
            Assert.Equal(expectedRecipeDTO.Ingredients, actualRecipeDTO.Ingredients);
            Assert.Equal(expectedRecipeDTO.Instructions, actualRecipeDTO.Instructions);
        }

        [Fact]
        public void AddIngredient_WithValidString_ShouldAddToIngredients()
        {
            RecipeEvent.NewIngredient = "sugar";

            RecipeEvent.AddIngredient(RecipeDTO);

            Assert.Equal("sugar", RecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddIngredient_WithInvalidString_ShouldNotValidate()
        {
            var validationContext = new ValidationContext(RecipeEvent)
            {
                MemberName = "NewIngredient"
            };

            RecipeEvent.NewIngredient = "The core idea in object-oriented programming is to " +
                "divide programs into smaller pieces and make each piece responsible for managing " +
                "its own state.";
            
            bool propertyIsValid = Validator.TryValidateProperty(RecipeEvent.NewIngredient, validationContext, new List<ValidationResult>());
            
            RecipeEvent.AddIngredient(RecipeDTO);
            
            Assert.False(propertyIsValid);
            Assert.Null(RecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddIngredient_WithEmptyString_ShouldValidate()
        {
            var validationContext = new ValidationContext(RecipeEvent)
            {
                MemberName = "NewIngredient"
            };

            RecipeEvent.NewIngredient = null;

            RecipeEvent.AddIngredient(RecipeDTO);

            bool propertyIsValid = Validator.TryValidateProperty(RecipeEvent.NewIngredient, validationContext, new List<ValidationResult>());

            Assert.True(propertyIsValid);
            Assert.Null(RecipeDTO.Ingredients.FirstOrDefault());
        }

        [Fact]
        public void AddInstruction_WithValidString_ShouldAddToInstructions()
        {
            RecipeEvent.NewInstruction = "Different pieces of such a program interact with each other through interfaces," +
                                           " limited sets of functions or bindings that provide useful functionality " +
                                           "at a more abstract level, hiding their precise implementation. Such program " +
                                           "pieces are modeled using objects. Their interface consists of a specific set of " +
                                           "methods and properties.";

            RecipeEvent.AddIngredient(RecipeDTO);

            Assert.Equal(RecipeEvent.NewIngredient, RecipeDTO.Ingredients.FirstOrDefault());
        }


        [Fact]
        public void AddInstruction_WithInvalidString_ShouldNotValidate()
        {
            var validationContext = new ValidationContext(RecipeEvent)
            {
                MemberName = "NewInstruction"
            };

            RecipeEvent.NewInstruction = "When called, that method should return an object that provides a second interface," +
                                          " iterator. This is the actual thing that iterates. It has a next method that returns" +
                                          " the next result. That result should be an object with a value property that provides" +
                                          " the next value, if there is one, and a done property, which should be true when there" +
                                          " are no more results and false otherwise.";

            bool propertyIsValid = Validator.TryValidateProperty(RecipeEvent.NewInstruction, validationContext, new List<ValidationResult>());

            RecipeEvent.AddInstruction(RecipeDTO);
            
            Assert.False(propertyIsValid);
            Assert.Null(RecipeDTO.Instructions.FirstOrDefault());
        }

        [Fact]
        public void AddInstruction_WithEmptyString_ShouldValidate()
        {
            var validationContext = new ValidationContext(RecipeEvent)
            {
                MemberName = "NewInstruction"
            };

            RecipeEvent.NewInstruction = null;

            RecipeEvent.AddInstruction(RecipeDTO);

            bool propertyIsValid = Validator.TryValidateProperty(RecipeEvent.NewInstruction, validationContext, new List<ValidationResult>());

            Assert.True(propertyIsValid);
            Assert.Null(RecipeDTO.Instructions.FirstOrDefault());
        }
    }
}
