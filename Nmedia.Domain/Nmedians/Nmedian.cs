#nullable enable
using System;

namespace Nmedia.Domain.Nmedians
{
  public class Nmedian
  {
    public short? Age { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTime? Hired { get; set; }
    public decimal? HourlyRate { get; set; }
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public JobTitle? JobTitle { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Picture { get; set; }
    public string? Slug { get; set; }
    public DateTimeOffset? Updated { get; set; }
    public Guid Uuid { get; set; }
  }
}
