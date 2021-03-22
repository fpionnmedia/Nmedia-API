#nullable enable
using System;

namespace Nmedia.Domain.Users
{
  public class User
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; }
    public Guid Token { get; set; }
    public string Username { get; set; } = string.Empty;
  }
}
