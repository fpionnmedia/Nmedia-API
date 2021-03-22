#nullable enable
using Microsoft.Extensions.Options;
using Nmedia.Api.Application.Users;
using Nmedia.Api.Persistence.Altitude3.Configuration;
using Nmedia.Domain.Users;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Persistence.Altitude3.Users
{
  public class SecurityService : ISecurityService
  {
    private readonly AltitudeOptions _altitudeOptions;
    private readonly HttpClient _client;

    public SecurityService(IOptions<AltitudeOptions> altitudeOptions, HttpClient client)
    {
      _altitudeOptions = altitudeOptions?.Value ?? throw new ArgumentNullException(nameof(altitudeOptions));
      _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<User> AuthenticateAsync(string refreshToken, CancellationToken cancellationToken)
    {
      var token = Guid.Parse(refreshToken);

      var requestUri = new Uri("/authenticate", UriKind.Relative);
      using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
      request.Headers.Add("Authorization", $"Bearer {token}");

      using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();
      string json = await response.Content.ReadAsStringAsync();
      SessionState state = JsonSerializer.Deserialize<SessionState>(json)!;

      return new User
      {
        Id = state.UserGuid,
        Name = state.PersonName,
        Role = GetRole(state.UserType),
        Token = token,
        Username = state.Username
      };
    }

    public async Task<User> LogInAsync(string username, string password, CancellationToken cancellationToken)
    {
      var requestUri = new Uri("/log-in", UriKind.Relative);
      using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
      request.Content = new JsonContent(new
      {
        password,
        siteGuid = _altitudeOptions.SiteGuid,
        username
      });

      using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();
      string json = await response.Content.ReadAsStringAsync();
      LogInResult result = JsonSerializer.Deserialize<LogInResult>(json)!;

      return new User
      {
        Id = result.SessionState.UserGuid,
        Name = result.SessionState.PersonName,
        Role = GetRole(result.SessionState.UserType),
        Token = result.TokenGuid,
        Username = result.SessionState.Username
      };
    }

    private static Role GetRole(string userTypeName)
    {
      UserType userType = Enum.Parse<UserType>(userTypeName);

      return userType switch
      {
        UserType.MasterUser => Role.Master,
        _ => Role.Standard,
      };
    }
  }
}
