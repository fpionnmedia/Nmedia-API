#nullable enable

namespace Nmedia.Api.Application
{
  public static class StringHelper
  {
    public static string? CleanTrim(this string s)
    {
      return string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    }
  }
}
