#nullable enable
using HotChocolate;
using HotChocolate.Data;
using Nmedia.Api.Persistence.Npgsql;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;
using System.Linq;

namespace Nmedia.Api.Web.Graph
{
  public class Query
  {
    [UseDbContext(typeof(NmediaContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Article> GetArticles([ScopedService] NmediaContext dbContext)
    {
      return dbContext.Articles;
    }

    [UseDbContext(typeof(NmediaContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Nmedian> GetNmedians([ScopedService] NmediaContext dbContext)
    {
      return dbContext.Nmedians;
    }
  }
}
