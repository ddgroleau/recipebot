using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.Data
{
    public class RecipeRepositoryTests : IDisposable
    {
        public ApplicationDbContext Db;
        public IUserState UserState;
        public MockObject MockObject;
        public IRecipeRepository RecipeRepository;
        public AbstractRecipeFactory RecipeFactory;

        public RecipeRepositoryTests()
        {
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            MockObject = new MockObject();
            RecipeFactory = new RecipeFactory();
            RecipeRepository = new RecipeRepository(RecipeFactory, Db, UserState);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
        
        [Fact]
        public async Task GetUserRecipes_WithOneSubscription_ShouldReturnSubscription()
        {
            var expected = MockObject.Recipe;
            var subscription = MockObject.RecipeSubscription;
            var userId = await UserState.CurrentUserIdAsync();

            await Db.Recipes.AddAsync(expected);
            await Db.RecipeSubscriptions.AddAsync(subscription);
            await Db.SaveChangesAsync();

            var result = await RecipeRepository.GetUserRecipes();
            var actual = result.Single();

            Assert.Equal(expected.RecipeId, actual.RecipeId);
            Assert.Equal(expected.RecipeType, actual.RecipeType);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.URL, actual.URL);
            Assert.Equal(expected.Ingredients.FirstOrDefault().Description, actual.Ingredients.FirstOrDefault());
            Assert.Equal(expected.Instructions.FirstOrDefault().Description, actual.Instructions.FirstOrDefault());
        }
    }
}
