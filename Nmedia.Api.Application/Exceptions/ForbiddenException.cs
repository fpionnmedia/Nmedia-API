#nullable enable
using System.Net;

namespace Nmedia.Api.Application.Exceptions
{
  public class ForbiddenException : ApiException
  {
    public ForbiddenException() : base(HttpStatusCode.Forbidden)
    {
    }
  }
}
