#nullable enable
using MediatR;
using Microsoft.Extensions.Options;
using Nmedia.Api.Application.Configuration;
using Nmedia.Api.Application.Users.Models;
using Nmedia.Domain.Users;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Users.Commands
{
  public class LogInHandler : IRequestHandler<LogIn, TokenModel>
  {
    private readonly ISecurityService _securityService;
    private readonly TokenOptions _tokenOptions;
    private readonly ITokenService _tokenService;

    public LogInHandler(
      ISecurityService securityService,
      IOptions<TokenOptions> tokenOptions,
      ITokenService tokenService
    )
    {
      _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
      _tokenOptions = tokenOptions?.Value ?? throw new ArgumentNullException(nameof(tokenOptions));
      _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<TokenModel> Handle(LogIn request, CancellationToken cancellationToken)
    {
      User user = await _securityService.LogInAsync(
        request.Model.Username,
        request.Model.Password,
        cancellationToken
      );

      return new TokenModel
      {
        AccessToken = _tokenService.Create(new ClaimsIdentity(new[]
        {
          new Claim("name", user.Name),
          new Claim("role", user.Role.ToString()),
          new Claim("sub", user.Id.ToString()),
          new Claim("username", user.Username)
        }), _tokenOptions.Lifetime),
        ExpiresIn = _tokenOptions.Lifetime,
        RefreshToken = user.Token.ToString(),
        TokenType = _tokenOptions.Type
      };
    }
  }
}
