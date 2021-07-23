using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeUrlDtoTests
    {
        [Fact]
        public void ResetURL_WithUrlValue_ShouldMakeUrlNull()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeUrlDTO>();
            var recipeUrlDTO = new RecipeUrlDTO(logger);

            recipeUrlDTO.URL = "https://www.google.com";

            recipeUrlDTO.ResetURL();

            Assert.Null(recipeUrlDTO.URL);
        }

        [Fact]
        public void PostRecipeURL_WithValidArguments_ShouldReturnRecipeDTO()
        {
            var recipeUrlDtoLogger = new LoggerFactory().CreateLogger<RecipeUrlDTO>();
            var recipeDtoLogger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var http = new HttpClient();
            var recipeUrlDTO = new RecipeUrlDTO(recipeUrlDtoLogger);
            var recipeDTO = new RecipeDTO(recipeDtoLogger);

            var result = recipeUrlDTO.PostRecipeUrl(http, recipeUrlDTO, recipeDTO);

            Assert.IsType<Task<IRecipeDTO>>(result);
        }
    }
}
