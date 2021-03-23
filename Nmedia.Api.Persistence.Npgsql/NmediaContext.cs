using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;

namespace Nmedia.Api.Persistence.Npgsql
{
  public class NmediaContext : DbContext, INmediaContext
  {
    public NmediaContext(DbContextOptions<NmediaContext> options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Nmedian> Nmedians { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
  }
}
