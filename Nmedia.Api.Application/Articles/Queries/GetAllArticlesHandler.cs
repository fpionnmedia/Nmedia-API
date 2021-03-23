#nullable enable
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Articles.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Articles.Queries
{
  public class GetAllArticlesHandler : IRequestHandler<GetAllArticles, IEnumerable<ArticleItemModel>>
  {
    private readonly INmediaContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllArticlesHandler(INmediaContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<ArticleItemModel>> Handle(GetAllArticles request, CancellationToken cancellationToken)
    {
      return await _dbContext.Articles
        .ProjectTo<ArticleItemModel>(_mapper.ConfigurationProvider)
        .ToArrayAsync(cancellationToken);
    }
  }
}
