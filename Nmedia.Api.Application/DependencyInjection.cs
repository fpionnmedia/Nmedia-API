#nullable enable
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Nmedia.Api.Application
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      Assembly assembly = typeof(DependencyInjection).Assembly;

      return services
        .AddAutoMapper(assembly)
        .AddMediatR(assembly);
    }
  }
}
