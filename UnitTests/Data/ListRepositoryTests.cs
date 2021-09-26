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
    public class ListRepositoryTests : IDisposable
    {
        public ApplicationDbContext Db;
        public IUserState UserState;
        public MockObject MockObject;
        public MockListObject MockListObject;
        public IListRepository ListRepository;
        public AbstractListFactory ListFactory;

        public ListRepositoryTests() 
        {
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            MockObject = new MockObject();
            MockListObject = new MockListObject();
            ListFactory = new ListFactory();
            ListRepository = new ListRepository(Db, UserState, ListFactory);
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

            var expectedDay1 = expected.ListDays.ElementAt(0);
            var expectedDay2 = expected.ListDays.ElementAt(1);
            var expectedDay3 = expected.ListDays.ElementAt(2);

            var actualDay1 = actual.ListDays.ElementAt(0);
            var actualDay2 = actual.ListDays.ElementAt(1);
            var actualDay3 = actual.ListDays.ElementAt(2);


            Assert.Equal(expected.ListEntityId, actual.ListEntityId);
            Assert.Equal(expected.Days,         actual.Days);
            Assert.Equal(expected.CreatedBy,    actual.CreatedBy);
            Assert.Equal(expected.CreatedOn,    actual.CreatedOn);

            Assert.Equal(expectedDay1.ListDayId,         actualDay1.ListDayId);
            Assert.Equal(expectedDay1.ListEntityId,      actualDay1.ListEntityId);
            Assert.Equal(expectedDay1.Date,              actualDay1.Date);
            Assert.Equal(expectedDay1.SequenceNumber,    actualDay1.SequenceNumber);
            Assert.Equal(expectedDay1.BreakfastRecipeId, actualDay1.BreakfastRecipeId);
            Assert.Equal(expectedDay1.LunchRecipeId,     actualDay1.LunchRecipeId);
            Assert.Equal(expectedDay1.DinnerRecipeId,    actualDay1.DinnerRecipeId);

            Assert.Equal(expectedDay2.ListDayId,         actualDay2.ListDayId);
            Assert.Equal(expectedDay2.ListEntityId,      actualDay2.ListEntityId);
            Assert.Equal(expectedDay2.Date,              actualDay2.Date);
            Assert.Equal(expectedDay2.SequenceNumber,    actualDay2.SequenceNumber);
            Assert.Equal(expectedDay2.BreakfastRecipeId, actualDay2.BreakfastRecipeId);
            Assert.Equal(expectedDay2.LunchRecipeId,     actualDay2.LunchRecipeId);
            Assert.Equal(expectedDay2.DinnerRecipeId,    actualDay2.DinnerRecipeId);

            Assert.Equal(expectedDay3.ListDayId,         actualDay2.ListDayId);
            Assert.Equal(expectedDay3.ListEntityId,      actualDay2.ListEntityId);
            Assert.Equal(expectedDay3.Date,              actualDay2.Date);
            Assert.Equal(expectedDay3.SequenceNumber,    actualDay2.SequenceNumber);
            Assert.Equal(expectedDay3.BreakfastRecipeId, actualDay2.BreakfastRecipeId);
            Assert.Equal(expectedDay3.LunchRecipeId,     actualDay2.LunchRecipeId);
            Assert.Equal(expectedDay3.DinnerRecipeId,    actualDay2.DinnerRecipeId);
        }
    }
}
