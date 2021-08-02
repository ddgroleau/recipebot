using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PBC.Shared;
using PBC.Shared.AccountComponent;
using PBC.Shared.DOM_Events;
using PBC.Shared.Lazor;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System.Linq;

namespace PBC.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IAccountChangesDTO, AccountChangesDTO>();
            services.AddScoped<IAccountRegisterDTO, AccountRegisterDTO>();
            services.AddScoped<IAccountLoginDTO, AccountLoginDTO>();
            services.AddScoped<IRecipeDTO, RecipeDTO>();
            services.AddScoped<IListGeneratorDTO, ListGeneratorDTO>();
            services.AddScoped<IListDayDTO, ListDayDTO>();
            services.AddScoped<IRecipeModel, RecipeModel>();
            services.AddScoped<IRepository<IRecipeEntity>, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeEntity, RecipeEntity>();
            services.AddScoped<IAllRecipesScraper, AllRecipesScraper>();
            services.AddScoped<IFactory<IIngredient>, IngredientFactory>();
            services.AddScoped<IFactory<IInstruction>, InstructionFactory>();
            services.AddScoped<IRecipeBuilder, RecipeBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
