namespace Nmedia.Api.Infrastructure.Configuration
{
  public class JwtOptions
  {
    public const string SectionName = "Jwt";

    public string Secret { get; set; }
  }
}
