#nullable enable
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Nmedians.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Nmedians.Queries
{
  public class GetAllNmediansHandler : IRequestHandler<GetAllNmedians, IEnumerable<NmedianItemModel>>
  {
    private readonly INmediaContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllNmediansHandler(INmediaContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<NmedianItemModel>> Handle(GetAllNmedians request, CancellationToken cancellationToken)
    {
      return await _dbContext.Nmedians
        .ProjectTo<NmedianItemModel>(_mapper.ConfigurationProvider)
        .ToArrayAsync(cancellationToken);
    }
  }
}
