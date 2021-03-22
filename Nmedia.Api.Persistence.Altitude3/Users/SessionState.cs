using System;
using System.Text.Json.Serialization;

namespace Nmedia.Api.Persistence.Altitude3.Users
{
  public class SessionState
  {
    [JsonPropertyName("contactGuid")]
    public Guid ContactGuid { get; set; }

    [JsonPropertyName("createdOn")]
    public DateTimeOffset CreatedOn { get; set; }

    [JsonPropertyName("personName")]
    public string PersonName { get; set; }

    [JsonPropertyName("siteConnectionString")]
    public string SiteConnectionString { get; set; }

    [JsonPropertyName("siteGuid")]
    public Guid SiteGuid { get; set; }

    [JsonPropertyName("siteName")]
    public string SiteName { get; set; }

    [JsonPropertyName("userConnectionString")]
    public string UserConnectionString { get; set; }

    [JsonPropertyName("userGuid")]
    public Guid UserGuid { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("userType")]
    public string UserType { get; set; }
  }
}
