using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared.Common;
using PBC.Shared.ListComponent;
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
    public class ListRepositoryTests
    {
        public ApplicationDbContext Db;
        public IUserState UserState;
        public MockObject MockObject;
        public MockListObject MockListObject;
        public IListRepository ListRepository;
        public AbstractRecipeFactory RecipeFactory;

        public ListRepositoryTests()
        {
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            MockObject = new MockObject();
            MockListObject = new MockListObject();
            RecipeFactory = new RecipeFactory();
            ListRepository = new ListRepository(
                                    RecipeFactory,
                                    Db,
                                    UserState);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task CreateListAsync_WithValidList_ShouldCreateList()
        {
            var generatedList = MockListObject.ListDTO;
            var expected = MockListObject.ListEntity;

            await ListRepository.CreateListAsync(generatedList);

            var actual = Db.Lists.FirstOrDefault();

            Assert.Equal(expected.Days, actual.Days);
        }
    }
}
