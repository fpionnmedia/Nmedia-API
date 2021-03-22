namespace Nmedia.Api.Application.Configuration
{
  public class TokenOptions
  {
    public const string SectionName = "Token";

    public int Lifetime { get; set; }
    public string Type { get; set; }
  }
}
