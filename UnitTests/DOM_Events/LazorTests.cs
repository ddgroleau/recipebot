using PBC.Shared;
using PBC.Shared.Lazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DOM_Events
{
    public class LazorTests
    {
        [Fact]
        public void Toggle_WithIsToggledDefault_ShouldMakeIsToggledFalse()
        {
            var lazor = new Lazor();

            lazor.Toggle();

            Assert.False(lazor.isToggled);
        }
        [Fact]
        public void Toggle_WithIsToggledAsFalse_ShouldMakeIsToggledTrue()
        {
            var lazor = new Lazor();
            lazor.isToggled = false;

            lazor.Toggle();

            Assert.True(lazor.isToggled);
        }
        [Fact]
        public void Toggle_WithToggleMethodRunTwice_ShouldMakeIsToggledTrue()
        {
            var lazor = new Lazor();

            lazor.Toggle();
            lazor.Toggle();

            Assert.True(lazor.isToggled);
        }

        [Fact]
        public void Hide_WithIsHiddenDefault_ShouldMakeIsHiddenTrue()
        {
            var lazor = new Lazor();

            lazor.Hide();

            Assert.True(lazor.isHidden);
        }

        [Fact]
        public void Show_WithIsShownDefault_ShouldMakeIsShownTrue()
        {
            var lazor = new Lazor();

            lazor.Show();

            Assert.True(lazor.isShown);
        }

        [Fact]
        public void IsPropertyValid_WithValidStringProperty_ShouldBeTrue()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject
            {
                URL = "https://www.allrecipes.com/recipe/212400/ginger-ale/"
            };

            bool isValid = lazor.IsPropertyValid(mockObj, "URL", mockObj.URL);

            Assert.True(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithValidNullStringProperty_ShouldBeTrue()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject
            {
                URL = ""
            };

            bool isValid = lazor.IsPropertyValid(mockObj, "URL", mockObj.URL);

            Assert.True(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithInvalidStringProperty_ShouldBeFalse()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject
            {
                URL = "1234"
            };

            bool isValid = lazor.IsPropertyValid(mockObj, "URL", mockObj.URL);

            Assert.False(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithValidListProperty_ShouldBeTrue()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject();
            mockObj.ListString.Add("TestItem");

            bool isValid = lazor.IsPropertyValid(mockObj, "ListString", mockObj.ListString);

            Assert.True(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithEmptyInvalidListProperty_ShouldBeFalse()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject();
            mockObj.ListString.Add(string.Empty);

            bool isValid = lazor.IsPropertyValid(mockObj, "ListString", mockObj.ListString);

            Assert.False(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithNullInvalidListProperty_ShouldBeFalse()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject();
            mockObj.ListString.Add(null);

            bool isValid = lazor.IsPropertyValid(mockObj, "ListString", mockObj.ListString);

            Assert.False(isValid);
        }

        [Fact]
        public void IsPropertyValid_WithoutListProperty_ShouldBeFalse()
        {
            var lazor = new Lazor();
            var mockObj = new MockObject();

            bool isValid = lazor.IsPropertyValid(mockObj, "ListString", mockObj.ListString);

            Assert.False(isValid);
        }

        [Fact]
        public void IsObjectValid_WithValidRecipeDTO_ShouldReturnTrue()
        {

            var recipeDTO = new RecipeDTO
            {
                URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/",
                Title = "Test",
                Description = "Test",
                Ingredients = new List<string>
                {
                    "test",
                    "test"
                },
                Instructions = new List<string>
                {
                    "test",
                    "test"
                }
            };
            var lazor = new Lazor();

            var isValid = lazor.IsObjectValid(recipeDTO);

            Assert.True(isValid);
        }

        [Fact]
        public void IsObjectValid_WithInvalidRecipeDTO_ShouldReturnFalse()
        {

            var recipeDTO = new RecipeDTO
            {
                URL = "https://www.allrecipes.com/recip",
                Title = "",
                Description = "Test",
                Ingredients = new List<string>(),
                Instructions = new List<string>()
            };
            var lazor = new Lazor();

            var isValid = lazor.IsObjectValid(recipeDTO);

            Assert.False(isValid);
        }

    }
}
