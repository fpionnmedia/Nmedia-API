#nullable enable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nmedia.Api.Application;

namespace Nmedia.Api.Persistence.Npgsql
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddNpgsql(this IServiceCollection services)
    {
      return services.AddDbContext<INmediaContext, NmediaContext>((provider, options) =>
      {
        var configuration = provider.GetRequiredService<IConfiguration>();
        options.UseNpgsql(configuration.GetConnectionString(nameof(NmediaContext)));
      });
    }
  }
}
