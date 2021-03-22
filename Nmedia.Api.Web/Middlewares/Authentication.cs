#nullable enable
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Nmedia.Api.Application.Configuration;
using Nmedia.Api.Application.Users;
using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
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
      HttpContext httpContext,
      ITokenService tokenService,
      IOptions<TokenOptions> tokenOptions
    )
    {
      HttpRequest request = httpContext.Request;
      HttpResponse response = httpContext.Response;

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

        try
        {
          tokenService.Validate(parts[1]);
        }
        catch (Exception)
        {
          await SetResponseAsync(response, type, "invalid_auth");
          return;
        }
      }
      else
      {
        await SetResponseAsync(response, type);
        return;
      }

      await _next(httpContext);
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
