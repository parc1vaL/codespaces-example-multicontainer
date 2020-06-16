using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Models;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                if (environment.EnvironmentName == "Codespaces")
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<BlogContext>())
                    {
                        context.Database.Migrate();

                        var testPost = context.Posts.FirstOrDefault();
                        if (testPost == null)
                        {
                            context.Posts.Add(new Post { Title = "Intro to codespaces" });
                            context.Posts.Add(new Post { Title = "More on codespaces" });
                        }

                        await context.SaveChangesAsync();
                    }
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
