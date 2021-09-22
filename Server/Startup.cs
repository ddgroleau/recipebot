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
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlite(
                 Configuration.GetConnectionString("SQLite")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>() //options => options.SignIn.RequireConfirmedAccount = true
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                    .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddHttpContextAccessor();

            services.AddScoped<IRecipeDTO, RecipeDTO>();
            services.AddScoped<IListGeneratorDTO, ListGeneratorDTO>();
            services.AddScoped<IListDayDTO, ListDayDTO>();
            services.AddScoped<IRecipeServiceDTO, RecipeServiceDTO>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IAllRecipesScraper, AllRecipesScraper>();
            services.AddScoped<IFactory<Ingredient>, IngredientFactory>();
            services.AddScoped<IFactory<Instruction>, InstructionFactory>();
            services.AddScoped<AbstractRecipeFactory, RecipeFactory>();
            services.AddScoped<IFactory<RecipeSubscription>, SubscriptionFactory>();
            services.AddScoped<IBuilder<IRecipeServiceDTO, IRecipeDTO>, RecipeBuilder>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<IListBuilder, ListBuilder>();
            services.AddScoped<IListDTO, ListDTO>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddScoped<HttpClient>();
            services.AddScoped<Recipe>();
            services.AddScoped<Ingredient>();
            services.AddScoped<Instruction>();
            services.AddScoped<RecipeSubscription>();

            services.AddSingleton<ISubscriberState, SubscriberState>();
            services.AddSingleton<IUserState, UserState>();
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
