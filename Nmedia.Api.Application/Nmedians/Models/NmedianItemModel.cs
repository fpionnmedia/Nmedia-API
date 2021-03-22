using System;

namespace Nmedia.Api.Application.Nmedians.Models
{
  public class NmedianItemModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset Updated { get; set; }
  }
}
