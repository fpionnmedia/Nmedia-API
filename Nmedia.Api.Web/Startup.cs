#nullable enable
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nmedia.Api.Application;
using Nmedia.Api.Application.Configuration;
using Nmedia.Api.Infrastructure;
using Nmedia.Api.Infrastructure.Configuration;
using Nmedia.Api.Persistence.Altitude3;
using Nmedia.Api.Persistence.Altitude3.Configuration;
using Nmedia.Api.Persistence.Npgsql;
using Nmedia.Api.Web.Configuration;
using Nmedia.Api.Web.Filters;
using Nmedia.Api.Web.Middlewares;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Nmedia.Api.Web
{
  public class Startup
  {
    private readonly IConfiguration _configuration;

    private readonly ApiConfiguration _apiConfiguration;
    private readonly CorsConfiguration _corsConfiguration;
    private readonly HashSet<string> _publicRoutes;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

      _apiConfiguration = configuration
        .GetSection(ApiConfiguration.SectionName)
        .Get<ApiConfiguration>();
      _corsConfiguration = configuration
        .GetSection(CorsConfiguration.SectionName)
        .Get<CorsConfiguration>();

      _publicRoutes = _apiConfiguration.PublicRoutes?.ToHashSet() ?? new HashSet<string>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAltitude3();
      services.AddApplication();
      services.AddControllers(options =>
      {
        options.Filters.Add(new ApiExceptionFilterAttribute());
        options.Filters.Add(new DbUpdateExceptionFilterAttribute());
      });
      services.AddCors();
      services.AddGraphQL();
      services.AddHttpContextAccessor();
      services.AddInfrastructure();
      services.AddNpgsql();
      services.AddSwagger(_apiConfiguration);

      services.Configure<AltitudeOptions>(_configuration.GetSection(AltitudeOptions.SectionName));
      services.Configure<JwtOptions>(_configuration.GetSection(JwtOptions.SectionName));
      services.Configure<TokenOptions>(_configuration.GetSection(TokenOptions.SectionName));

      services.AddSingleton<IApplicationContext, HttpApplicationContext>();

      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
      if (environment.IsDevelopment())
      {
        application.UseDeveloperExceptionPage();
        application.UseSwagger();
        application.UseSwaggerUI(config => config.SwaggerEndpoint(
          url: "/swagger/v1/swagger.json",
          name: $"{_apiConfiguration.Title} {_apiConfiguration.Version}"
        ));
      }

      application.UseHttpsRedirection();
      application.UseRouting();
      application.UseCors(policy => policy.Configure(_corsConfiguration));
      application.UseWhen(RequiresAuth, application => application.UseMiddleware<Authentication>());
      application.UseAuthorization();
      application.UseEndpoints(endpoints =>
      {
        endpoints.MapGraphQL();
        endpoints.MapControllers();
      });
      application.UseGraphQLVoyager();
    }

    private bool RequiresAuth(HttpContext context)
    {
      //PathString path = context.Request.Path;

      //if (path.HasValue)
      //{
      //  string trimmed = path.Value!.Trim('/');
      //  return !string.IsNullOrEmpty(trimmed) && !_publicRoutes.Contains(trimmed);
      //} // TODO: implement

      return false;
    }
  }
}
