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
            var recipeUrlDTO = new RecipeUrlDTO();

            recipeUrlDTO.URL = "https://www.google.com";

            recipeUrlDTO.ResetURL();

            Assert.Null(recipeUrlDTO.URL);
        }
    }
}
