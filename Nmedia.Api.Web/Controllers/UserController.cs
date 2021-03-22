#nullable enable
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nmedia.Api.Application.Users.Commands;
using Nmedia.Api.Application.Users.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Web.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : Controller
  {
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenModel>> LogInAsync(
      [FromBody] LogInModel model,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new LogIn(model), cancellationToken));
    }

    [HttpPost("renew")]
    public async Task<ActionResult<TokenModel>> RenewAsync(
      [FromBody] RenewModel model,
      CancellationToken cancellationToken
    )
    {
      return Ok(await _mediator.Send(new Renew(model), cancellationToken));
    }
  }
}
