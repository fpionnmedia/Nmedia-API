using Nmedia.Domain.Nmedians;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Api.Application.Nmedians.Models
{
  public class SaveNmedianModel
  {
    [Range(1, 100)]
    public short? Age { get; set; }
    
    public DateTime? Hired { get; set; }
    
    [MinValue(0)]
    public decimal? HourlyRate { get; set; }
    
    public bool IsActive { get; set; }
    
    [Enum(typeof(JobTitle))]
    public string JobTitle { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    public string Picture { get; set; }

    [MaxLength(100)]
    public string Slug { get; set; }
  }
}
