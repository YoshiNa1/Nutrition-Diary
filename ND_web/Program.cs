using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ND_web.Models;

namespace ND_web
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);

            host.Run();

        }


        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var app_context = services.GetRequiredService<ApplicationDbContext>();
                    var prof_context = services.GetRequiredService<ProfileContext>();
                    var food_context = services.GetRequiredService<FoodContext>();
                    var ration_context = services.GetRequiredService<RationContext>();
                    var meal_context = services.GetRequiredService<MealContext>();

                    app_context.Database.EnsureCreated();
                    prof_context.Database.EnsureCreated();
                    food_context.Database.EnsureCreated();
                    ration_context.Database.EnsureCreated();
                    meal_context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the db.");
                }
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}