using Nmedia.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Users
{
  public interface ISecurityService
  {
    Task<User> LogInAsync(string username, string password, CancellationToken cancellationToken);
  }
}
