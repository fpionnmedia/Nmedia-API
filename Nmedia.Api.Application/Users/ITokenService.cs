using System.Security.Claims;

namespace Nmedia.Api.Application.Users
{
  public interface ITokenService
  {
    string Create(ClaimsIdentity principal, int? lifetime = null);
    ClaimsPrincipal Validate(string token);
  }
}
