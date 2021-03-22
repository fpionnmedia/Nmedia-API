using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nmedia.Api.Application;
using Nmedia.Api.Application.Configuration;
using Nmedia.Api.Infrastructure;
using Nmedia.Api.Infrastructure.Configuration;
using Nmedia.Api.Persistence.Altitude3;
using Nmedia.Api.Persistence.Altitude3.Configuration;
using Nmedia.Api.Web.Configuration;
using System;

namespace Nmedia.Api.Web
{
  public class Startup
  {
    private readonly ApiConfiguration _apiConfiguration;
    private readonly CorsConfiguration _corsConfiguration;

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

      _apiConfiguration = configuration
        .GetSection(ApiConfiguration.SectionName)
        .Get<ApiConfiguration>();
      _corsConfiguration = configuration
        .GetSection(CorsConfiguration.SectionName)
        .Get<CorsConfiguration>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAltitude3();
      services.AddApplication();
      services.AddControllers();
      services.AddCors();
      services.AddInfrastructure();
      services.AddSwagger(_apiConfiguration);

      services.Configure<AltitudeOptions>(_configuration.GetSection(AltitudeOptions.SectionName));
      services.Configure<JwtOptions>(_configuration.GetSection(JwtOptions.SectionName));
      services.Configure<TokenOptions>(_configuration.GetSection(TokenOptions.SectionName));
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
      application.UseAuthorization();
      application.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
