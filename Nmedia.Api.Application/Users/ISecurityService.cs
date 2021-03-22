using Nmedia.Api.Application.Users.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Application.Users
{
  public interface ISecurityService
  {
    Task<UserModel> LogInAsync(string username, string password, CancellationToken cancellationToken);
  }
}
