using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.AccountComponent;
using PBC.Shared.DOM_Events;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.Lazor;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;


namespace PBC.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("PBC.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PBC.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddTransient<ILazor, Lazor>();
            
            builder.Services.AddScoped<IAccountChangesDTO, AccountChangesDTO>();
            builder.Services.AddScoped<IAccountRegisterDTO, AccountRegisterDTO>();
            builder.Services.AddScoped<IAccountLoginDTO, AccountLoginDTO>();
          
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
