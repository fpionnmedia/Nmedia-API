using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Nmedia.Api.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host
      .CreateDefaultBuilder(args)
      .ConfigureAppConfiguration(config => config.AddJsonFile("environment.json"))
      .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
  }
}
