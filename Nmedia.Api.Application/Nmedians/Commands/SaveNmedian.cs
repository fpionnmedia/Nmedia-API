#nullable enable
using MediatR;
using Nmedia.Api.Application.Nmedians.Models;
using System;

namespace Nmedia.Api.Application.Nmedians.Commands
{
  public class SaveNmedian : IRequest<NmedianModel>
  {
    public SaveNmedian(SaveNmedianModel model, Guid? id = null)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
      Id = id;
    }

    public Guid? Id { get; }
    public SaveNmedianModel Model { get; }
  }
}
