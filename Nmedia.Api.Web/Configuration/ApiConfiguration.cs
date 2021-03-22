namespace Nmedia.Api.Web.Configuration
{
  public class ApiConfiguration
  {
    public const string SectionName = "Api";

    public string[] PublicRoutes { get; set; }
    public string Title { get; set; }
    public string Version { get; set; }
  }
}
