using System;

namespace Nmedia.Api.Application.Users.Models
{
  public class UserModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid Token { get; set; }
    public string Username { get; set; }
  }
}
