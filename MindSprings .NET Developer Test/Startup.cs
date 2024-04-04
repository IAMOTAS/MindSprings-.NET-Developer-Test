using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MindSprings_.NET_Developer_Test.Models;
using MindSprings_.NET_Developer_Test.Services;

namespace MindSprings_.NET_Developer_Test
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
            // Configure Entity Framework Core with SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register your custom services
            services.AddHttpClient<TranslationService>();
            services.AddTransient<TranslationService>(); // Register TranslationService as transient
            services.AddHttpClient<FunTranslationsService>(); // Register FunTranslationsService as transient

            // Add controllers and views
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure middleware, routing, etc.
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Translation}/{action=Index}/{id?}");

                // Add a custom route for TranslationController
                endpoints.MapControllerRoute(
                    name: "translation",
                    pattern: "Translation/{action=Index}/{id?}");
            });
        }
    }
}
