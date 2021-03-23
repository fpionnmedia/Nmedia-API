#nullable enable
using Nmedia.Domain.Nmedians;
using System;

namespace Nmedia.Domain.Articles
{
  public class Article
  {
    public Category[]? Categories { get; set; }
    public string? Content { get; set; }
    public DateTimeOffset Created { get; set; }
    public int Id { get; set; }
    public Nmedian? Nmedian { get; set; }
    public int? NmedianId { get; set; }
    public string? Picture { get; set; }
    public DateTime? Published { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset? Updated { get; set; }
    public Guid Uuid { get; set; }
  }
}
