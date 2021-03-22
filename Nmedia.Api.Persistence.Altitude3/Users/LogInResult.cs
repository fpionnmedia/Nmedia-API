using System;
using System.Text.Json.Serialization;

namespace Nmedia.Api.Persistence.Altitude3.Users
{
  public class LogInResult
  {
    [JsonPropertyName("sessionState")]
    public SessionState SessionState { get; set; }

    [JsonPropertyName("tokenGuid")]
    public Guid TokenGuid { get; set; }
  }
}
