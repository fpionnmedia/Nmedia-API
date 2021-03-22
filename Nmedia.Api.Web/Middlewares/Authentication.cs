#nullable enable
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Nmedia.Api.Application.Configuration;
using Nmedia.Api.Application.Users;
using Nmedia.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nmedia.Api.Web.Middlewares
{
  public class Authentication
  {
    private readonly RequestDelegate _next;

    public Authentication(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(
      HttpContext context,
      ITokenService tokenService,
      IOptions<TokenOptions> tokenOptions
    )
    {
      HttpRequest request = context.Request;
      HttpResponse response = context.Response;

      string type = tokenOptions.Value.Type;

      if (request.Headers.TryGetValue("Authorization", out StringValues values))
      {
        string value = values.Single();
        string[] parts = value.Split();
        if (parts.Length != 2)
        {
          await SetResponseAsync(response, type, "invalid_header");
          return;
        }
        else if (!parts[0].Equals(tokenOptions.Value.Type, StringComparison.InvariantCultureIgnoreCase))
        {
          await SetResponseAsync(response, type, "not_supported");
          return;
        }

        ClaimsPrincipal principal;
        try
        {
          principal = tokenService.Validate(parts[1]);
        }
        catch (Exception)
        {
          await SetResponseAsync(response, type, "invalid_auth");
          return;
        }

        Dictionary<string, string> claims = principal.Claims.ToDictionary(x => x.Type, x => x.Value);
        var user = new User
        {
          Id = Guid.Parse(claims["sub"]),
          Name = claims["name"],
          Role = Enum.Parse<Role>(claims["role"]),
          Username = claims["username"]
        };

        if (!context.SetUser(user))
        {
          throw new InvalidOperationException("The HttpContext user could not be set.");
        }
      }
      else
      {
        await SetResponseAsync(response, type);
        return;
      }

      await _next(context);
    }

    private static async Task SetResponseAsync(HttpResponse response, string type, string? code = null)
    {
      response.Headers.Add("WWW-Authenticate", type);
      response.StatusCode = (int)HttpStatusCode.Unauthorized;

      if (code != null)
      {
        response.ContentType = MediaTypeNames.Application.Json;
        await response.WriteAsJsonAsync(new { code });
      }
    }
  }
}
