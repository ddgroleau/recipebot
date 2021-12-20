using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recipebot.Shared;
using Recipebot.Server.Models;
using Recipebot.Shared.Common;
using Recipebot.Shared.ListComponent;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.SubscriptionComponent;
using Recipebot.Shared.WebScraper;
using Recipebot.Server.Data;
using Recipebot.Server.Data.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace Recipebot.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Data
            if (Env.IsDevelopment())
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
               Configuration.GetConnectionString("DevelopmentDb"),
               o => o.MigrationsAssembly("Recipebot.Server")));
            } 
            else if (Env.IsStaging())
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
               Configuration.GetConnectionString("StagingDb"),
               o => o.MigrationsAssembly("Recipebot.Server")));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
               Configuration.GetConnectionString("ProductionDb"),
               o => o.MigrationsAssembly("Recipebot.Server")));
            }
            
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
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

            context.Database.Migrate();
        }
    }
}
