using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeMementoTests : IDisposable
    {
        IRecipeMemento RecipeMemento;
        ILogger<IRecipeMemento> Logger;

        public RecipeMementoTests()
        {
            Logger = new LoggerFactory().CreateLogger<IRecipeMemento>();
            RecipeMemento = new RecipeMemento(Logger);
        }
        public void Dispose()
        {
            Logger = new LoggerFactory().CreateLogger<IRecipeMemento>();
            RecipeMemento = new RecipeMemento(Logger);
        }

        [Fact]
        public async Task GetUserRecipesAsync_WithRecipesChangedIsTrue_ShouldReturnRecipeList()
        {
            var result = await RecipeMemento.GetUserRecipesAsync("userName");
            Assert.IsAssignableFrom<List<RecipeDTO>>(result);
        }

    }
}
