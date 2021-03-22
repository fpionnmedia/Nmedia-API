using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nmedia.Api.Application.Users;
using Nmedia.Api.Persistence.Altitude3.Configuration;
using Nmedia.Api.Persistence.Altitude3.Users;
using System;
using System.Net.Http;

namespace Nmedia.Api.Persistence.Altitude3
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddAltitude3(this IServiceCollection services)
    {
      return services
        .AddSingleton(provider =>
        {
          AltitudeOptions altitudeOptions = provider.GetRequiredService<IOptions<AltitudeOptions>>().Value;
          return new HttpClient
          {
            BaseAddress = new Uri(altitudeOptions.SecurityApiBaseUrl)
          };
        })
        .AddScoped<ISecurityService, SecurityService>();
    }
  }
}
