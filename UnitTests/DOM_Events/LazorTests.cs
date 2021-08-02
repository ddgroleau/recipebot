using PBC.Shared;
using PBC.Shared.DOM_Events;
using PBC.Shared.Lazor;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DOM_Events
{
    public class LazorTests : IDisposable
    {
        ILazor Lazor;
        MockObject MockObject;
        IRecipeDTO RecipeDTO;
        public LazorTests()
        {
            Lazor = new Lazor();
            MockObject = new MockObject();
            RecipeDTO = new RecipeDTO();
        }

        public void Dispose()
        {
            Lazor = new Lazor();
            MockObject = new MockObject();
            RecipeDTO = new RecipeDTO();
        }
        [Fact]
        public void Toggle_WithIsToggledDefault_ShouldMakeIsToggledFalse()
        {
            Lazor.Toggle();

            Assert.False(Lazor.IsToggled);
        }

        [Fact]
        public void Toggle_WithToggleMethodRunTwice_ShouldMakeIsToggledTrue()
        {
            Lazor.Toggle();
            Lazor.Toggle();

            Assert.True(Lazor.IsToggled);
        }

        [Fact]
        public void Hide_WithIsHiddenDefault_ShouldMakeIsHiddenTrue()
        {
            Lazor.Hide();

            Assert.True(Lazor.IsToggled);
        }

        [Fact]
        public void Show_WithIsShownDefault_ShouldMakeIsShownTrue()
        {
            Lazor.Show();

            Assert.True(Lazor.IsShown);
        }

        [Fact]
        public void IsPropertyValid_WithValidStringProperty_ShouldBeTrue()
        {
            MockObject.URL = "https://www.allrecipes.com/recipe/212400/ginger-ale/";

            bool isValid = Lazor.IsPropertyValid(MockObject, "URL", MockObject.URL);

            Assert.True(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithValidNullStringProperty_ShouldBeTrue()
        {
            MockObject.URL = "";

            bool isValid = Lazor.IsPropertyValid(MockObject, "URL", MockObject.URL);

            Assert.True(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithInvalidStringProperty_ShouldBeFalse()
        {
            MockObject.URL = "1234";

            bool isValid = Lazor.IsPropertyValid(MockObject, "URL", MockObject.URL);

            Assert.False(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithValidListProperty_ShouldBeTrue()
        {
            MockObject.ListString.Add("TestItem");

            bool isValid = Lazor.IsPropertyValid(MockObject, "ListString", MockObject.ListString);

            Assert.True(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithEmptyInvalidListProperty_ShouldBeFalse()
        {
            MockObject.ListString.Add(string.Empty);

            bool isValid = Lazor.IsPropertyValid(MockObject, "ListString", MockObject.ListString);

            Assert.False(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithNullInvalidListProperty_ShouldBeFalse()
        {
            MockObject.ListString.Add(null);

            bool isValid = Lazor.IsPropertyValid(MockObject, "ListString", MockObject.ListString);

            Assert.False(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithoutListProperty_ShouldBeFalse()
        {
            bool isValid = Lazor.IsPropertyValid(MockObject, "ListString", MockObject.ListString);

            Assert.False(isValid);
        }

        [Fact]
        public void IsObjectValid_WithValidRecipeDTO_ShouldReturnTrue()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();
            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var isValid = Lazor.IsObjectValid(recipeDTO);

            Assert.True(isValid);
        }

        [Fact]
        public void IsObjectValid_WithInvalidRecipeDTO_ShouldReturnFalse()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/";
            recipeDTO.Title = "";
            recipeDTO.Description = "Test";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            var isValid = Lazor.IsObjectValid(recipeDTO);

            Assert.False(isValid);
        }

    }
}
