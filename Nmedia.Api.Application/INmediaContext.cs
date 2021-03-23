using Microsoft.EntityFrameworkCore;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application
{
  public interface INmediaContext
  {
    DbSet<Article> Articles { get; }
    DbSet<Nmedian> Nmedians { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
