#nullable enable
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Articles.Models;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;
using Nmedia.Domain.Users;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Articles.Commands
{
  public class SaveArticleHandler : IRequestHandler<SaveArticle, ArticleModel>
  {
    private readonly IApplicationContext _appContext;
    private readonly IDateTimeOffset _dateTimeOffset;
    private readonly INmediaContext _dbContext;
    private readonly IGuid _guid;
    private readonly IMapper _mapper;

    public SaveArticleHandler(
      IApplicationContext appContext,
      IDateTimeOffset dateTimeOffset,
      INmediaContext dbContext,
      IGuid guid,
      IMapper mapper
    )
    {
      _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
      _dateTimeOffset = dateTimeOffset ?? throw new ArgumentNullException(nameof(dateTimeOffset));
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _guid = guid ?? throw new ArgumentNullException(nameof(guid));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ArticleModel> Handle(SaveArticle request, CancellationToken cancellationToken)
    {
      if (_appContext.User.Role != Role.Master)
      {
        throw new ForbiddenException();
      }

      Nmedian? nmedian = null;
      if (request.Model.NmedianId.HasValue)
      {
        nmedian = await _dbContext.Nmedians
          .SingleOrDefaultAsync(x => x.Uuid == request.Model.NmedianId.Value, cancellationToken)
          ?? throw new NotFoundException(nameof(request.Model.NmedianId));
      }

      Article entity;
      if (request.Id.HasValue)
      {
        entity = await _dbContext.Articles
          .SingleOrDefaultAsync(x => x.Uuid == request.Id.Value, cancellationToken)
          ?? throw new NotFoundException();

        entity.Updated = _dateTimeOffset.Now;
      }
      else
      {
        entity = new Article
        {
          Created = _dateTimeOffset.Now,
          Uuid = _guid.NewGuid()
        };

        _dbContext.Articles.Add(entity);
      }

      entity.Categories = request.Model.Categories
        ?.Select(category => Enum.Parse<Category>(category))
        .Distinct()
        .ToArray();
      entity.Content = request.Model.Content.CleanTrim();
      entity.Nmedian = nmedian;
      entity.NmedianId = nmedian?.Id;
      entity.Picture = request.Model.Picture?.CleanTrim();
      entity.Published = request.Model.Published;
      entity.Title = request.Model.Title?.CleanTrim() ?? throw new InvalidOperationException("The Title is required.");

      await _dbContext.SaveChangesAsync(cancellationToken);

      return _mapper.Map<ArticleModel>(entity);
    }
  }
}
