using PBC.Shared;
using PBC.Shared.DOM_Events;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.Lazor;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ListComponent
{
    public class ListGeneratorEventTests : IDisposable
    {
        ILazor Lazor;
        IListGeneratorDTO ListGeneratorDTO;
        IListGeneratorEvent Event;

        public ListGeneratorEventTests()
        {
            Lazor = new Lazor();
            ListGeneratorDTO = new ListGeneratorDTO();
            Event = new ListGeneratorEvent(Lazor, ListGeneratorDTO);
        }

        public void Dispose()
        {
            Lazor = new Lazor();
            ListGeneratorDTO = new ListGeneratorDTO();
            Event = new ListGeneratorEvent(Lazor, ListGeneratorDTO);
        }

        [Fact] 
        public void RemoveDay_WithDaysEqualsSix_ShouldRemoveDayAndListDayDTO()
        {
            Event.ListGeneratorDTO.Days = 6;
            Event.ListGeneratorDTO.GeneratedDays.Add(Event.ListGeneratorDTO.Days, new ListDayDTO());

            Event.RemoveDay();

            Assert.True(Event.ListGeneratorDTO.Days == 5);
            Assert.False(Event.ListGeneratorDTO.GeneratedDays.Any());
        }

        [Fact]
        public void RemoveDay_WithDaysEqualsZero_ShouldSetErrorMessage()
        {

            Event.ListGeneratorDTO.Days = 0;

            Event.RemoveDay();

            Assert.True(Event.ListGeneratorDTO.Days == 0);
            Assert.Equal("Min 0 Days", Event.Lazor.ErrorMessage);
        }
    }
}
