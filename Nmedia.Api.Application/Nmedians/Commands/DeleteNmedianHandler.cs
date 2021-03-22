#nullable enable
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application.Exceptions;
using Nmedia.Domain.Nmedians;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Nmedians.Commands
{
  public class DeleteNmedianHandler : IRequestHandler<DeleteNmedian>
  {
    private readonly INmediaContext _dbContext;

    public DeleteNmedianHandler(INmediaContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(DeleteNmedian request, CancellationToken cancellationToken)
    {
      Nmedian entity = await _dbContext.Nmedians
        .SingleOrDefaultAsync(x => x.Uuid == request.Id, cancellationToken)
        ?? throw new NotFoundException();

      _dbContext.Nmedians.Remove(entity);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
