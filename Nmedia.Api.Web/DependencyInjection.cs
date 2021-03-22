using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nmedia.Api.Web.Configuration;
using System;

namespace Nmedia.Api.Web
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddSwagger(this IServiceCollection services, ApiConfiguration apiConfiguration)
    {
      return services.AddSwaggerGen(config =>
      {
        config.SwaggerDoc(apiConfiguration.Version, new OpenApiInfo
        {
          Contact = new OpenApiContact
          {
            Email = "fpion@nmedia.ca",
            Name = "Francis Pion",
            Url = new Uri("https://github.com/fpionnmedia")
          },
          Description = "Nmedia website Web API.",
          License = new OpenApiLicense
          {
            Name = "Use under MIT",
            Url = new Uri("https://github.com/fpionnmedia/nmedia-api/blob/master/LICENSE")
          },
          Title = apiConfiguration.Title,
          Version = apiConfiguration.Version
        });
      });
    }

    public static void Configure(this CorsPolicyBuilder builder, CorsConfiguration configuration)
    {
      if (configuration.AllowedOrigins == null)
      {
        builder.AllowAnyOrigin();
      }
      else
      {
        builder.WithOrigins(configuration.AllowedOrigins);
      }

      if (configuration.AllowedMethods == null)
      {
        builder.AllowAnyMethod();
      }
      else
      {
        builder.WithMethods(configuration.AllowedMethods);
      }

      if (configuration.AllowedHeaders == null)
      {
        builder.AllowAnyHeader();
      }
      else
      {
        builder.WithExposedHeaders(configuration.AllowedHeaders);
      }
    }
  }
}
