using Microsoft.Extensions.Logging;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
        public async Task AddDay()
        {
            Lazor.SetErrorMessage(null);
            if (ListGeneratorDTO.Days >= 7)
            {
                Lazor.SetErrorMessage("Max 7 Days");
            }
            else
            {
                try
                {
                    ListGeneratorDTO.Days += 1;
                    var listDay = ListDayDTO;
                    listDay = await _http.GetFromJsonAsync<ListDayDTO>("/api/list/day");
                    ListGeneratorDTO.GeneratedDays.Add(ListGeneratorDTO.Days, listDay);
                }
                    catch (Exception)
                {
                    _logger.LogError($"Could not retrieve random recipe from ListController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}");
                }
            }

        }
        public void RemoveDay()
        {
            Lazor.SetErrorMessage(null);
            if (ListGeneratorDTO.Days <= 0)
            {
                Lazor.SetErrorMessage("Min 0 Days");
            }
            else
            {
                ListGeneratorDTO.GeneratedDays.Remove(ListGeneratorDTO.Days);
                ListGeneratorDTO.Days -= 1;
            }
        }
    }
}
