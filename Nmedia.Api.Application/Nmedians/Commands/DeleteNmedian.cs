#nullable enable
using MediatR;
using System;

namespace Nmedia.Api.Application.Nmedians.Commands
{
  public class DeleteNmedian : IRequest
  {
    public DeleteNmedian(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
