#nullable enable
using Nmedia.Domain.Articles;
using System;
using System.Collections.Generic;

namespace Nmedia.Domain.Nmedians
{
  public class Nmedian
  {
    public short? Age { get; set; }
    public ICollection<Article>? Articles { get; set; } = new List<Article>();
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
