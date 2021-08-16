using PBC.Shared;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.ListComponent
{
    public class ListServiceTests : IDisposable
    {
        IListRepository ListRepository;
        IRecipeDTO RecipeDTO;
        IListService ListService;
        IListDTO ListDTO;
        IListDayDTO ListDayDTO;
        IListBuilder ListBuilder;
        HttpClient Http;
        IListGeneratorDTO ListGeneratorDTO;
        MockListObject MockList;
  
        public ListServiceTests()
        {
            ListRepository = new ListRepository();
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListDTO = new ListDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO, ListDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO, ListDTO, ListRepository);
            ListGeneratorDTO = new ListGeneratorDTO();
            MockList = new MockListObject();
        }

        public void Dispose()
        {
            ListRepository = new ListRepository();
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListDTO = new ListDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO, ListDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO, ListDTO, ListRepository);
            ListGeneratorDTO = new ListGeneratorDTO();
            MockList = new MockListObject();
        }

        [Fact]
        public async void GenerateDayOfRecipes_NoParameters_ShouldReturnDayDTO()
        {
            var result = await ListService.GenerateDayOfRecipes();

            Assert.IsAssignableFrom<IListDayDTO>(result);
        }

        [Fact]
        public void CreateList_WithEmptyListGeneratorDTO_ShouldReturnListDTO()
        {
            var list = ListService.CreateList(ListGeneratorDTO);

            Assert.IsAssignableFrom<IListDTO>(list);
        }

        [Fact]
        public void CreateList_WithValidListGeneratorDTO_ShouldHaveEqualObjects()
        {
            var list = ListService.CreateList(MockList.GeneratedList);

            Assert.Equal(MockList.GeneratedList.Days, list.Days);
            Assert.Equal(MockList.GeneratedList.GeneratedDays.Count, list.ListDays.ToList().Count);
        }

        [Fact]
        public void CreateList_WithInvalidListGeneratorDTO_ShouldHaveNullDays()
        {
            var list = ListService.CreateList(MockList.InvalidList);

            Assert.Equal(0,list.Days);
        }
    }
}
