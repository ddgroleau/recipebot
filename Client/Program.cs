using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Recipebot.Shared;
using Recipebot.Shared.DOM_Events;
using Recipebot.Shared.DOM_Events.ComponentEvents;
using Recipebot.Shared.Lazor;
using Recipebot.Shared.ListComponent;
using Recipebot.Shared.RecipeComponent;

namespace Recipebot.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("Recipebot.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Recipebot.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddTransient<ILazor, Lazor>();
            
            builder.Services.AddScoped<IListGeneratorDTO, ListGeneratorDTO>();
            builder.Services.AddScoped<IListDayDTO, ListDayDTO>();
            
            builder.Services.AddScoped<IRecipeUrlDTO, RecipeUrlDTO>();
            builder.Services.AddScoped<IRecipeDTO, RecipeDTO>();
            
            builder.Services.AddScoped<IMessageModalEvent, MessageModalEvent>();
            builder.Services.AddScoped<ICreateRecipeEvent, CreateRecipeEvent>();
            builder.Services.AddScoped<IEditRecipeEvent, EditRecipeEvent>();
            builder.Services.AddScoped<ICookBookTableEvent, CookbookTableEvent>();
            builder.Services.AddScoped<ISearchBarEvent, SearchBarEvent>();
            builder.Services.AddScoped<IListGeneratorEvent, ListGeneratorEvent>();
            builder.Services.AddScoped<IGeneratedDayEvent, GeneratedDayEvent>();

            builder.Services.AddScoped<IEnumerable<IRecipeDTO>, List<RecipeDTO>>();

            builder.Services.AddSingleton<RecipeTypes>();

            await builder.Build().RunAsync();
        }
    }
}
