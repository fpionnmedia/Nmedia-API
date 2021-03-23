#nullable enable
using MediatR;
using Nmedia.Api.Application.Users.Models;
using System;

namespace Nmedia.Api.Application.Users.Commands
{
  public class LogOut : IRequest
  {
    public LogOut(LogOutModel model)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public LogOutModel Model { get; }
  }
}
