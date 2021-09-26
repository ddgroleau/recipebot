using Microsoft.Extensions.Logging;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared;
using PBC.Shared.Common;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Data;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.ListComponent
{
    public class ListServiceTests : IDisposable
    {
        public AbstractListFactory ListFactory;
        public ApplicationDbContext Db;
        public IUserState UserState;
        ILogger<ISubscriberState> StateLogger;
        ISubscriberState SubscriberState;
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
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(StateLogger);
            ListFactory = new ListFactory();
            ListRepository = new ListRepository(Db,UserState,ListFactory);
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListDTO = new ListDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO, ListDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO, ListDTO, ListRepository, SubscriberState);
            ListGeneratorDTO = new ListGeneratorDTO();
            MockList = new MockListObject();
        }

        public void Dispose()
        {
            Http.Dispose();
            GC.SuppressFinalize(this);
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
        public async Task CreateList_WithValidListGeneratorDTO_ShouldHaveEqualObjects()
        {
            var list = await ListService.CreateList(MockList.GeneratedList);

            Assert.Equal(MockList.GeneratedList.Days, list.Days);
            Assert.Equal(MockList.GeneratedList.GeneratedDays.Count, list.ListDays.ToList().Count);
        }

        [Fact]
        public async Task CreateList_WithInvalidListGeneratorDTO_ShouldHaveNullDays()
        {
            var list = await ListService.CreateList(MockList.InvalidList);

            Assert.Equal(0, list.Days);
        }

        [Fact]
        public async Task GenerateRandomRecipeByType_WithValidRecipeType_ShouldReturnRecipeDTO()
        {
            var recipe = await ListService.GenerateRandomRecipeByType("Breakfast");

            Assert.IsAssignableFrom<IRecipeDTO>(recipe);
            Assert.Null(recipe.Title);
        }
    }
}
