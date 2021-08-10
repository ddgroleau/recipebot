﻿using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.DOM_Events;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.Lazor;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DOM_Events.ComponentEvents
{
    public class ListGeneratorEventTests : IDisposable
    {
        HttpClient Http;
        ILogger<ListGeneratorEvent> Logger;
        ILazor Lazor;
        IListGeneratorDTO ListGeneratorDTO;
        IListDayDTO ListDayDTO;
        IListGeneratorEvent Event;

        public ListGeneratorEventTests()
        {
            Logger = new LoggerFactory().CreateLogger<ListGeneratorEvent>();
            Http = new HttpClient();
            Lazor = new Lazor();
            ListGeneratorDTO = new ListGeneratorDTO();
            ListDayDTO = new ListDayDTO();
            Event = new ListGeneratorEvent(Lazor, ListGeneratorDTO, ListDayDTO, Http, Logger);
        }

        public void Dispose()
        {
            Logger = new LoggerFactory().CreateLogger<ListGeneratorEvent>();
            Http = new HttpClient();
            Lazor = new Lazor();
            ListGeneratorDTO = new ListGeneratorDTO();
            ListDayDTO = new ListDayDTO();
            Event = new ListGeneratorEvent(Lazor, ListGeneratorDTO, ListDayDTO, Http, Logger);
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

        [Fact]
        public void AddDay_WithValidDays_ShouldAddDay()
        {
            Event.ListGeneratorDTO.Days = 2;

            Event.AddDay();

            Assert.Equal(3, Event.ListGeneratorDTO.Days);
        }
    }
}