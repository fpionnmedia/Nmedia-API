#nullable enable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nmedia.Domain.Nmedians;

namespace Nmedia.Api.Persistence.Npgsql.Configurations
{
  public class NmedianConfiguration : IEntityTypeConfiguration<Nmedian>
  {
    public void Configure(EntityTypeBuilder<Nmedian> builder)
    {
      builder.Property(x => x.HourlyRate).HasColumnType("DECIMAL(5,2)");
    }
  }
}
