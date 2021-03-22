#nullable enable
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Nmedia.Api.Persistence.Altitude3
{
  public class JsonContent : StringContent
  {
    public JsonContent(object value, Encoding? encoding = null) : base(
      content: JsonSerializer.Serialize(value),
      encoding: encoding ?? Encoding.UTF8,
      mediaType: MediaTypeNames.Application.Json
    )
    {
    }
  }
}
