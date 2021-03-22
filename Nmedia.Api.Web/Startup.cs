using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
      services.AddControllers();
      services.AddCors();
      services.AddSwagger(_apiConfiguration);
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
