#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Nmedia.Api.Application;
using Nmedia.Api.Application.Users;
using Nmedia.Api.Infrastructure.Users;

namespace Nmedia.Api.Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      return services
        .AddSingleton<IDateTimeOffset, DateTimeOffsetService>()
        .AddSingleton<IGuid, GuidService>()
        .AddSingleton<ITokenService, JwtService>();
    }
  }
}
