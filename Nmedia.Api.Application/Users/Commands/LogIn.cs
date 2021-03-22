#nullable enable
using MediatR;
using Nmedia.Api.Application.Users.Models;
using System;

namespace Nmedia.Api.Application.Users.Commands
{
  public class LogIn : IRequest<TokenModel>
  {
    public LogIn(LogInModel model)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public LogInModel Model { get; }
  }
}
