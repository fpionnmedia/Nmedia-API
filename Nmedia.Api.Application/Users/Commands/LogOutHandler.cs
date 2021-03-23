#nullable enable
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Users.Commands
{
  public class LogOutHandler : IRequestHandler<LogOut>
  {
    private readonly ISecurityService _securityService;

    public LogOutHandler(ISecurityService securityService)
    {
      _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
    }

    public async Task<Unit> Handle(LogOut request, CancellationToken cancellationToken)
    {
      await _securityService.LogOutAsync(request.Model.RefreshToken, cancellationToken);

      return Unit.Value;
    }
  }
}
