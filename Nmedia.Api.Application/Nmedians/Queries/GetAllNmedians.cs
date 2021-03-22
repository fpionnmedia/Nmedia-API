#nullable enable
using MediatR;
using Nmedia.Api.Application.Nmedians.Models;
using System.Collections.Generic;

namespace Nmedia.Api.Application.Nmedians.Queries
{
  public class GetAllNmedians : IRequest<IEnumerable<NmedianItemModel>>
  {
  }
}
