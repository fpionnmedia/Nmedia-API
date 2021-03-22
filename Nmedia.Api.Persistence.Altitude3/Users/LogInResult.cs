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

  public class SessionState
  {
    [JsonPropertyName("personName")]
    public string PersonName { get; set; }

    [JsonPropertyName("userGuid")]
    public Guid UserGuid { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }
  }
}
