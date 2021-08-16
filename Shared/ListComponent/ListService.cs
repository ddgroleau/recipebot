using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{

    public class ListService : IListService
    {
        private readonly IListBuilder _listBuilder;
        private readonly HttpClient _http;
        private readonly IListDayDTO _listDayDTO;
        public ListService(IListBuilder listBuilder, HttpClient http, IListDayDTO listDayDTO)
        {
            _listBuilder = listBuilder;
            _http = http;
            _listDayDTO = listDayDTO;
        }

        
        public async Task<IListDayDTO> GenerateDayOfRecipes()
        {
            string userName = "Test"; //Remove this once auth is implemented

            try
            {
                var userRecipes = await _http.GetFromJsonAsync<List<RecipeDTO>>($"https://localhost:4001/api/Recipe/UserRecipes/{userName}");

                return _listBuilder.Build(userRecipes);

            }
            catch (Exception)
            {
                return _listDayDTO;
            }
            
        }
    }
}
