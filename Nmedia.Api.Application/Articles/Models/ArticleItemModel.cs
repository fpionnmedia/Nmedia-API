using System;

namespace Nmedia.Api.Application.Articles.Models
{
  public class ArticleItemModel
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset Updated { get; set; }
  }
}
