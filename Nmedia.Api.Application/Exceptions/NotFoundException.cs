#nullable enable
using System.Net;

namespace Nmedia.Api.Application.Exceptions
{
  public class NotFoundException : ApiException
  {
    public NotFoundException(string? field = null) : base(HttpStatusCode.NotFound)
    {
      if (field != null)
      {
        Value = new { field };
      }
    }
  }
}
