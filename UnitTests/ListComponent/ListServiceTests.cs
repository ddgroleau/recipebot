using PBC.Shared;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ListComponent
{
    public class ListServiceTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        IListService ListService;
        IListDayDTO ListDayDTO;
        IListBuilder ListBuilder;
        HttpClient Http;
  
        public ListServiceTests()
        {
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO);
        }

        public void Dispose()
        {
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO);
        }
        [Fact]
        public async void GenerateDayOfRecipes_NoParameters_ShouldReturnDayDTO()
        {
            var result = await ListService.GenerateDayOfRecipes();

            Assert.IsAssignableFrom<IListDayDTO>(result);
        }
    }
}
