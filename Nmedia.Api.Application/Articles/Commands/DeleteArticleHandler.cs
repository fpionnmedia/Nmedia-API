#nullable enable
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Articles.Commands
{
  public class DeleteArticleHandler : IRequestHandler<DeleteArticle>
  {
    private readonly IApplicationContext _appContext;
    private readonly INmediaContext _dbContext;

    public DeleteArticleHandler(IApplicationContext appContext, INmediaContext dbContext)
    {
      _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(DeleteArticle request, CancellationToken cancellationToken)
    {
      Article entity = await _dbContext.Articles
        .SingleOrDefaultAsync(x => x.Uuid == request.Id, cancellationToken)
        ?? throw new NotFoundException();

      if (_appContext.User.Role != Role.Master)
      {
        throw new ForbiddenException();
      }

      _dbContext.Articles.Remove(entity);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
