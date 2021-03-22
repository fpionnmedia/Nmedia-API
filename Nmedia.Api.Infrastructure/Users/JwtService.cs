#nullable enable
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nmedia.Api.Application.Users;
using Nmedia.Api.Infrastructure.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nmedia.Api.Infrastructure.Users
{
  public class JwtService : ITokenService
  {
    private readonly SecurityKey _key;
    private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

    public JwtService(IOptions<JwtOptions> options)
    {
      byte[] key = Encoding.ASCII.GetBytes(options.Value.Secret);
      _key = new SymmetricSecurityKey(key);
    }

    public string Create(ClaimsIdentity subject, int? lifetime = null)
    {
      DateTime? expires = null;
      if (lifetime.HasValue)
      {
        expires = DateTime.UtcNow.AddSeconds(lifetime.Value);
      }

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Expires = expires,
        SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature),
        Subject = subject
      };

      SecurityToken token = _tokenHandler.CreateToken(tokenDescriptor);

      return _tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal Validate(string token)
    {
      return _tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        IssuerSigningKey = _key,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true
      }, out _);
    }
  }
}
