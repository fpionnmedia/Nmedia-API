using System;

namespace Nmedia.Api.Application.Articles.Models
{
  public class ArticleModel
  {
    public string[] Categories { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Created { get; set; }
    public Guid Id { get; set; }
    public Guid? NmedianId { get; set; }
    public string Picture { get; set; }
    public DateTime? Published { get; set; }
    public string Title { get; set; }
    public DateTimeOffset? Updated { get; set; }
  }
}
