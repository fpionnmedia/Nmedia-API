using Nmedia.Domain.Users;

namespace Nmedia.Api.Application
{
  public interface IApplicationContext
  {
    User User { get; }
  }
}
