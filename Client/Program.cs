using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.AccountComponent;
using PBC.Shared.DOM_Events;
using PBC.Shared.Lazor;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<ILazor, Lazor>();
            
            builder.Services.AddScoped<IAccountChangesDTO, AccountChangesDTO>();
            builder.Services.AddScoped<IAccountRegisterDTO, AccountRegisterDTO>();
            builder.Services.AddScoped<IAccountLoginDTO, AccountLoginDTO>();
            builder.Services.AddScoped<IRecipeDTO, RecipeDTO>();
            builder.Services.AddScoped<IListGeneratorDTO, ListGeneratorDTO>();
            builder.Services.AddScoped<IListDayDTO, ListDayDTO>();
           
            await builder.Build().RunAsync();
        }
    }
}
