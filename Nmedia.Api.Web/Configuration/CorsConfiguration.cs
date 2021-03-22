﻿namespace Nmedia.Api.Web.Configuration
{
  public class CorsConfiguration
  {
    public const string SectionName = "Cors";

    public string[] AllowedHeaders { get; set; }
    public string[] AllowedMethods { get; set; }
    public string[] AllowedOrigins { get; set; }
  }
}
