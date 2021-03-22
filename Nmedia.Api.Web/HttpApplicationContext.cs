#nullable enable
using Microsoft.AspNetCore.Http;
using Nmedia.Api.Application;
using Nmedia.Domain.Users;
using System;

namespace Nmedia.Api.Web
{
  public class HttpApplicationContext : IApplicationContext
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpApplicationContext(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public User? User => _httpContextAccessor.HttpContext!.GetUser();
  }
}
