using System;

namespace Nmedia.Api.Persistence.Altitude3.Configuration
{
  public class AltitudeOptions
  {
    public const string SectionName = "Altitude";

    public string SecurityApiBaseUrl { get; set; }
    public Guid SiteGuid { get; set; }
  }
}
