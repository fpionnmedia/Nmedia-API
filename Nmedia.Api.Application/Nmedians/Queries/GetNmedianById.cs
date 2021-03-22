#nullable enable
using MediatR;
using Nmedia.Api.Application.Nmedians.Models;
using System;

namespace Nmedia.Api.Application.Nmedians.Queries
{
  public class GetNmedianById : IRequest<NmedianModel>
  {
    public GetNmedianById(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
