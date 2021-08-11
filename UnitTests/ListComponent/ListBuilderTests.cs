using PBC.Shared;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ListComponent
{
    public class ListBuilderTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        IListDayDTO ListDayDTO;
        ListBuilder ListBuilder;
        IEnumerable<IRecipeDTO> UserRecipes;
        IEnumerable<IRecipeDTO> EmptyUserRecipes;
        public ListBuilderTests()
        {
            RecipeDTO = new RecipeDTO();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO);
            UserRecipes = new List<IRecipeDTO>
            {
               new RecipeDTO { Title = $"Recipe1", Description = "Description1", RecipeType="Breakfast" },
               new RecipeDTO { Title = $"Recipe2", Description = "Description2", RecipeType="Breakfast" },
               new RecipeDTO { Title = $"Recipe3", Description = "Description3", RecipeType="Breakfast" },
               new RecipeDTO { Title = $"Recipe4", Description = "Description4", RecipeType="Breakfast" },
                new RecipeDTO { Title = $"Recipe5", Description = "Description5", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe6", Description = "Description6", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe7", Description = "Description7", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe8", Description = "Description8", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe9", Description = "Description9", RecipeType="Dinner"},
                new RecipeDTO { Title = $"Recipe10", Description = "Description10", RecipeType="Dinner"},
                new RecipeDTO { Title = $"Recipe11", Description = "Description11", RecipeType="Dinner"},
                new RecipeDTO { Title = $"Recipe12", Description = "Description12", RecipeType="Dinner"},
            };
            EmptyUserRecipes = new List<IRecipeDTO>();
        }

        public void Dispose()
        {
            RecipeDTO = new RecipeDTO();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO);
            UserRecipes = new List<IRecipeDTO>();
            EmptyUserRecipes = new List<IRecipeDTO>();
        }

        [Fact]
        public void GenerateRandomRecipeByType_WithBreakfastParameter_ShouldReturnRandomRecipe()
        {
            var actual = ListBuilder.GenerateRandomRecipeByType(UserRecipes, "Breakfast");

            Assert.IsAssignableFrom<IRecipeDTO>(actual);
            Assert.Equal("Breakfast", actual.RecipeType);
        }
        [Fact]
        public void GenerateRandomRecipeByType_WithLunchParameter_ShouldReturnRandomRecipe()
        {
            var actual = ListBuilder.GenerateRandomRecipeByType(UserRecipes, "Lunch").RecipeType;

            Assert.Equal("Lunch", actual);
        }
        [Fact]
        public void GenerateRandomRecipeByType_WithDinnerParameter_ShouldReturnRandomRecipe()
        {
            var actual = ListBuilder.GenerateRandomRecipeByType(UserRecipes, "Dinner").RecipeType;

            Assert.Equal("Dinner", actual);
        }
        [Fact]
        public void GenerateRandomRecipeByType_WithEmptyList_ShouldReturnEmptyRecipeDTO()
        {
            var actual = ListBuilder.GenerateRandomRecipeByType(EmptyUserRecipes, "Dinner");

            Assert.Null(actual.Title);
        }
        [Fact]
        public void Build_WithValidUserRecipes_ShouldReturnListDayDTO()
        {
            var listDay = ListBuilder.Build(UserRecipes);

            Assert.IsAssignableFrom<IListDayDTO>(listDay);
        }
        [Fact]
        public void Build_WithEmptyUserRecipes_ShouldReturnListDayDTO()
        {
            var listDay = ListBuilder.Build(EmptyUserRecipes);

            Assert.IsAssignableFrom<IListDayDTO>(listDay);
            Assert.Null(listDay.Breakfast.Title);
            Assert.Null(listDay.Lunch.Title);
            Assert.Null(listDay.Dinner.Title);
        }
    }
}
