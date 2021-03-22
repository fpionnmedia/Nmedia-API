#nullable enable
using MediatR;
using Nmedia.Api.Application.Users.Models;
using System;

namespace Nmedia.Api.Application.Users.Commands
{
  public class Renew : IRequest<TokenModel>
  {
    public Renew(RenewModel model)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public RenewModel Model { get; }
  }
}
