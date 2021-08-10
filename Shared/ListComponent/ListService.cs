using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
  
    public class ListService : IListService
    {
        private readonly IListBuilder _listBuilder;
        private readonly HttpClient _http;
        public ListService(IListBuilder listBuilder, HttpClient http)
        {
            _listBuilder = listBuilder;
            _http = http;
        }

        public async Task<IListDayDTO> GenerateDayOfRecipes()
        {
            var userRecipes = await _http.GetFromJsonAsync<List<RecipeDTO>>("/api/Recipe/UserRecipes/{username}");
            return _listBuilder.Build(userRecipes);
        }

    }
}
