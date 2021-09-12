using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PBC.Shared;
using PBC.Shared.AccountComponent;
using PBC.Shared.Common;
using PBC.Shared.Common.Data;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using PBC.Shared.WebScraper;
using System.Net.Http;

namespace PBC.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            services.AddScoped<IRecipeServiceDTO, RecipeServiceDTO>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IAllRecipesScraper, AllRecipesScraper>();
            services.AddScoped<IFactory<Ingredient>, IngredientFactory>();
            services.AddScoped<IFactory<Instruction>, InstructionFactory>();
            services.AddScoped<IFactory<RecipeSubscription>, SubscriptionFactory>();
            services.AddScoped<IBuilder<IRecipeServiceDTO, IRecipeDTO>, RecipeBuilder>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<IListBuilder, ListBuilder>();
            services.AddScoped<IListDTO, ListDTO>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubscriptionServiceDTO, SubscriptionServiceDTO>();

            services.AddScoped<HttpClient>();
            services.AddScoped<Recipe>();
            services.AddScoped<Ingredient>();
            services.AddScoped<Instruction>();
            services.AddScoped<RecipeSubscription>();

            services.AddSingleton<ISubscriberState, SubscriberState>();

            services.AddDbContext<StagingDbContext>(options =>
                 options.UseSqlite(
                     Configuration.GetConnectionString("SQLiteStaging")));
      
            services.AddDbContext<DevDbContext>(options =>
                  options.UseSqlite(
                      Configuration.GetConnectionString("SQLiteDev")));

            services.AddDbContext<ProdDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("SQLiteProd")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddEntityFrameworkStores<DevDbContext>();
        }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
