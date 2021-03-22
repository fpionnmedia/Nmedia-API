#nullable enable
using Microsoft.Extensions.Options;
using Nmedia.Api.Application.Users;
using Nmedia.Api.Application.Users.Models;
using Nmedia.Api.Persistence.Altitude3.Configuration;
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

    public SecurityService(
      IOptions<AltitudeOptions> altitudeOptions, HttpClient client )
    {
      _altitudeOptions = altitudeOptions?.Value ?? throw new ArgumentNullException(nameof(altitudeOptions));
      _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<UserModel> LogInAsync(string username, string password, CancellationToken cancellationToken)
    {
      var requestUri = new Uri("/log-in", UriKind.Relative);
      using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
      request.Content = new JsonContent(new { password, username });
      request.Headers.Add("SiteGuid", _altitudeOptions.SiteGuid.ToString());

      using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();
      string json = await response.Content.ReadAsStringAsync();
      LogInResult result = JsonSerializer.Deserialize<LogInResult>(json)!;

      return new UserModel
      {
        Id = result.SessionState.UserGuid,
        Name = result.SessionState.PersonName,
        Token = result.TokenGuid,
        Username = result.SessionState.Username
      };
    }
  }
}
