using Nmedia.Domain.Articles;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Api.Application.Articles.Models
{
  public class SaveArticleModel
  {
    [Enum(typeof(Category))]
    public string[] Categories { get; set; }
    
    public string Content { get; set; }
    
    public Guid? NmedianId { get; set; }
    
    public string Picture { get; set; }
    
    public DateTime? Published { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
  }
}
