#nullable enable
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Articles.Models;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Domain.Articles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Articles.Queries
{
  public class GetArticleByIdHandler : IRequestHandler<GetArticleById, ArticleModel>
  {
    private readonly INmediaContext _dbContext;
    private readonly IMapper _mapper;

    public GetArticleByIdHandler(INmediaContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ArticleModel> Handle(GetArticleById request, CancellationToken cancellationToken)
    {
      Article entity = await _dbContext.Articles
        .Include(x => x.Nmedian)
        .SingleOrDefaultAsync(x => x.Uuid == request.Id, cancellationToken)
        ?? throw new NotFoundException();

      return _mapper.Map<ArticleModel>(entity);
    }
  }
}
