#nullable enable
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Domain.Nmedians;
using Nmedia.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Nmedians.Commands
{
  public class DeleteNmedianHandler : IRequestHandler<DeleteNmedian>
  {
    private readonly IApplicationContext _appContext;
    private readonly INmediaContext _dbContext;

    public DeleteNmedianHandler(IApplicationContext appContext, INmediaContext dbContext)
    {
      _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(DeleteNmedian request, CancellationToken cancellationToken)
    {
      Nmedian entity = await _dbContext.Nmedians
        .SingleOrDefaultAsync(x => x.Uuid == request.Id, cancellationToken)
        ?? throw new NotFoundException();

      if (_appContext.User.Role != Role.Master)
      {
        throw new ForbiddenException();
      }

      _dbContext.Nmedians.Remove(entity);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
