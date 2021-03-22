using System.ComponentModel.DataAnnotations;

namespace Nmedia.Api.Application.Users.Models
{
  public class LogInModel
  {
    [Required]
    public string Password { get; set; }

    [Required]
    public string Username { get; set; }
  }
}
