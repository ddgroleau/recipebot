using Microsoft.EntityFrameworkCore;
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
        public IFactory<Ingredient> IngredientFactory;
        public IFactory<Instruction> InstructionFactory;
        public ApplicationDbContext Db;
        public IUserState UserState;
        public MockObject MockObject;
        public IRecipeRepository RecipeRepository;
        public AbstractRecipeFactory RecipeFactory;

        public RecipeRepositoryTests()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            MockObject = new MockObject();
            RecipeFactory = new RecipeFactory();
            RecipeRepository = new RecipeRepository(
                                    RecipeFactory, 
                                    Db, 
                                    UserState, 
                                    IngredientFactory, 
                                    InstructionFactory);
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

        [Fact]
        public async Task GetUserRecipes_WithNoSubscriptions_ShouldReturnEmptyList()
        {
            var result = await RecipeRepository.GetUserRecipes();

            bool isEmpty = !result.Any();

            Assert.True(isEmpty);
        }

        [Fact]
        public async Task CreateRecipe_WithValidRecipe_ShouldCreateRecipe()
        {
            var userId = await UserState.CurrentUserIdAsync();
            var recipe = MockObject.RecipeServiceDTO;
            var expected = MockObject.CreateRecipeExpected;

            await RecipeRepository.CreateRecipe(recipe);

            var createdRecipe = await Db.Recipes
                .Include(x => x.Ingredients)
                .Include(x => x.Ingredients)
                .Where(x => x.URL == recipe.URL)
                .FirstOrDefaultAsync();

            Assert.Equal(1,                    createdRecipe.RecipeId);
            Assert.Equal(expected.RecipeType,  createdRecipe.RecipeType);
            Assert.Equal(expected.Title,       createdRecipe.Title);
            Assert.Equal(expected.Description, createdRecipe.Description);
            Assert.Equal(expected.URL,         createdRecipe.URL);
            Assert.Equal(expected.CreatedBy,   createdRecipe.CreatedBy);
           
            Assert.Equal(1,                                            createdRecipe.Ingredients.Single().RecipeId);
            Assert.Equal(expected.Ingredients.Single().IngredientId,   createdRecipe.Ingredients.Single().IngredientId);
            Assert.Equal(expected.Ingredients.Single().CreatedBy,      createdRecipe.Ingredients.Single().CreatedBy);
            Assert.Equal(expected.Ingredients.Single().Description,    createdRecipe.Ingredients.Single().Description);
                                                                       
            Assert.Equal(1,                                            createdRecipe.Instructions.Single().RecipeId);
            Assert.Equal(expected.Instructions.Single().InstructionId, createdRecipe.Instructions.Single().InstructionId);
            Assert.Equal(expected.Instructions.Single().CreatedBy,     createdRecipe.Instructions.Single().CreatedBy);
            Assert.Equal(expected.Instructions.Single().Description,   createdRecipe.Instructions.Single().Description);
            Assert.Equal(expected.Instructions.Single().StepNumber,    createdRecipe.Instructions.Single().StepNumber);

            Assert.Equal(
                String.Format("{0:MM/dd/yyyy HH:mm}", expected.Ingredients.Single().CreatedOn), 
                String.Format("{0:MM/dd/yyyy HH:mm}", createdRecipe.Ingredients.Single().CreatedOn));
            Assert.Equal(
                String.Format("{0:MM/dd/yyyy HH:mm}", expected.Instructions.Single().CreatedOn),
                String.Format("{0:MM/dd/yyyy HH:mm}", createdRecipe.Instructions.Single().CreatedOn));
            Assert.Equal(
               String.Format("{0:MM/dd/yyyy HH:mm}", expected.CreatedOn),
               String.Format("{0:MM/dd/yyyy HH:mm}", createdRecipe.CreatedOn));
        }

    }
}
