﻿using System.Text.Json.Serialization;

namespace Nmedia.Api.Application.Users.Models
{
  public class LogOutModel
  {
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
  }
}
