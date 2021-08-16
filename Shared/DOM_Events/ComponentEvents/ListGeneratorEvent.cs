﻿using Microsoft.Extensions.Logging;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public class ListGeneratorEvent : IListGeneratorEvent
    {
        public ILazor Lazor { get; set; }
        public IListGeneratorDTO ListGeneratorDTO { get; set; }
        public IListDayDTO ListDayDTO { get; set; }

        private readonly HttpClient _http;
        private readonly ILogger<ListGeneratorEvent> _logger;

        public ListGeneratorEvent(ILazor lazor, IListGeneratorDTO listGeneratorDTO, IListDayDTO listDayDTO, HttpClient http, ILogger<ListGeneratorEvent> logger)
        {
            Lazor = lazor;
            ListGeneratorDTO = listGeneratorDTO;
            ListDayDTO = listDayDTO;
            _http = http;
            _logger = logger;
        }

        public async Task SubmitList()
        {
            try
            {
                PrepareInstanceState();
                var response = await _http.PostAsJsonAsync("/api/List/NewList", new List<ListDayDTO>()); // Need to determne why the concrete imp is returning 400.
                if (response.IsSuccessStatusCode)
                {
                    Lazor.SetSuccessStatus(true);
                }
                else
                {
                    _logger.LogError($"Failure to post new list to ListController. Server responded with {response.StatusCode}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    Lazor.SetErrorMessage("List submission failed. Please try again.");
                }
            }
            catch (Exception e)
            {
                Lazor.SetErrorMessage("List submission failed. Please try again.");
                _logger.LogError($"Failure to post new list to ListController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. {e.Message}.");
            }
            finally
            {
                Lazor.SetLoadingStatus(false);
            }
        }

        public async Task<IListGeneratorDTO> AddDay()
        {
            PrepareInstanceState();
            if (ListGeneratorDTO.Days >= 7)
            {
                Lazor.SetErrorMessage("Max 7 Days");
            }
            else
            {
                var listDay = await GenerateRandomDay();
                ListGeneratorDTO.Days += 1;
                ListGeneratorDTO.GeneratedDays.Add(listDay);
            }
            Lazor.SetLoadingStatus(false);
            return ListGeneratorDTO;
        }

        public IListGeneratorDTO RemoveDay()
        {
            PrepareInstanceState();
            if (ListGeneratorDTO.Days <= 0)
            {
                Lazor.SetErrorMessage("Min 0 Days");
            }
            else
            {
                ListGeneratorDTO.GeneratedDays.Remove(ListGeneratorDTO.GeneratedDays
                                              .ElementAt(ListGeneratorDTO.GeneratedDays
                                              .Count - 1));
                ListGeneratorDTO.Days -= 1;
            }
            Lazor.SetLoadingStatus(false);
            return ListGeneratorDTO;
        }

        private async Task<ListDayDTO> GenerateRandomDay()
        {
            try
            {
                var listDay = await _http.GetFromJsonAsync<ListDayDTO>("/api/List/Day");
                listDay.Date = listDay.Date.AddDays(ListGeneratorDTO.Days+1);
                listDay.SequenceNumber = ListGeneratorDTO.Days+1;
                return listDay;
            }
            catch (Exception e)
            {
                _logger.LogError($" Could not retrieve random recipe from ListController at ListGeneratorEvent. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. {e.Message}.");
            }
            return (ListDayDTO)ListDayDTO;
        }

        private void PrepareInstanceState()
        {
            Lazor.SetLoadingStatus(true);
            Lazor.SetErrorMessage(null);
            Lazor.SetSuccessStatus(false);
        }
    }
}
