#nullable enable
using System.Net;

namespace Nmedia.Api.Application.Exceptions
{
  public class NotFoundException : ApiException
  {
    public NotFoundException() : base(HttpStatusCode.NotFound)
    {
    }
  }
}
