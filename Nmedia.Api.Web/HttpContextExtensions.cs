#nullable enable
using Microsoft.AspNetCore.Http;
using Nmedia.Domain.Users;
using System.Collections.Generic;

namespace Nmedia.Api.Web
{
  public static class HttpContextExtensions
  {
    private const string UserKey = nameof(User);

    public static User? GetUser(this HttpContext context)
    {
      if (context.Items.TryGetValue(UserKey, out object? value))
      {
        return (User?)value;
      }

      return null;
    }

    public static bool SetUser(this HttpContext context, User? user)
    {
      if (context.Items.ContainsKey(UserKey))
      {
        if (!context.Items.Remove(UserKey))
        {
          return false;
        }
      }

      return user != null && context.Items.TryAdd(UserKey, user);
    }
  }
}
