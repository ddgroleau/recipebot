using PBC.Shared;
using PBC.Shared.Lazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ListComponent
{
    public class ListGeneratorDTOTests
    {
        [Fact] 
        public void RemoveDay_WithDaysEqualsSix_ShouldRemoveDayAndListDayDTO()
        {
            var listGenDTO = new ListGeneratorDTO();
            var lazor = new Lazor();

            listGenDTO.Days = 6;
            listGenDTO.GeneratedDays.Add(listGenDTO.Days, new ListDayDTO());

            listGenDTO.RemoveDay(lazor);

            Assert.True(listGenDTO.Days == 5);
            Assert.False(listGenDTO.GeneratedDays.Any());
        }

        [Fact]
        public void RemoveDay_WithDaysEqualsZero_ShouldSetErrorMessage()
        {
            var listGenDTO = new ListGeneratorDTO();
            var lazor = new Lazor();

            listGenDTO.Days = 0;

            listGenDTO.RemoveDay(lazor);

            Assert.True(listGenDTO.Days == 0);
            Assert.Equal("Min 0 Days", lazor.ErrorMessage);
        }
    }
}
