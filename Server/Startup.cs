using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using PBC.Shared;
using PBC.Server.Models;
using PBC.Shared.Common;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using PBC.Shared.WebScraper;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

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
            #region Data
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
            Configuration.GetConnectionString("SQLite")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IListRepository, ListRepository>();
            #endregion

            #region Identity
            services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                    .AddIdentityServerJwt();
            #endregion

            #region Utility
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddSingleton<IUserState, UserState>();
            #endregion

            #region RecipeComponent
            services.AddScoped<IRecipeDTO, RecipeDTO>();
            services.AddScoped<IRecipeServiceDTO, RecipeServiceDTO>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IAllRecipesScraper, AllRecipesScraper>();
            services.AddScoped<IFactory<Ingredient>, IngredientFactory>();
            services.AddScoped<IFactory<Instruction>, InstructionFactory>();
            services.AddScoped<AbstractRecipeFactory, RecipeFactory>();
            services.AddScoped<IBuilder<IRecipeServiceDTO, IRecipeDTO>, RecipeBuilder>();
            services.AddScoped<Recipe>();
            services.AddScoped<Ingredient>();
            services.AddScoped<Instruction>();
            #endregion

            #region ListComponent
            services.AddScoped<IListService, ListService>();
            services.AddScoped<IListBuilder, ListBuilder>();
            services.AddScoped<IListDTO, ListDTO>();
            services.AddScoped<IListDayDTO, ListDayDTO>();
            services.AddScoped<IListGeneratorDTO, ListGeneratorDTO>();
            services.AddScoped<AbstractListFactory, ListFactory>();
            #endregion

            #region SubscriptionComponent
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IFactory<RecipeSubscription>, SubscriptionFactory>();
            services.AddSingleton<ISubscriberState, SubscriberState>();
            services.AddScoped<RecipeSubscription>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
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
            app.UseIdentityServer();
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
