using System;

namespace Nmedia.Api.Application.Nmedians.Models
{
  public class NmedianModel
  {
    public short? Age { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTime? Hired { get; set; }
    public decimal? HourlyRate { get; set; }
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string JobTitle { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Slug { get; set; }
    public DateTimeOffset? Updated { get; set; }
  }
}
