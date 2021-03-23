#nullable enable
using HotChocolate;
using HotChocolate.Types;
using Nmedia.Api.Persistence.Npgsql;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;
using System.Linq;

namespace Nmedia.Api.Web.Graph.Types
{
  public class ArticleType : ObjectType<Article>
  {
    protected override void Configure(IObjectTypeDescriptor<Article> descriptor)
    {
      descriptor.Description("Represents an article of the Nmédia blog.");

      descriptor.Field(x => x.Id).Ignore();
      descriptor.Field(x => x.Uuid).Name("id")
        .Description("Represents the unique identifier of the article.");

      descriptor.Field(x => x.Categories).Description("Represents the categories of the article.");
      descriptor.Field(x => x.Content).Description("Represents the article content in HTML.");
      descriptor.Field(x => x.Created).Description("Represents the creation timestamp of the article.");
      descriptor.Field(x => x.NmedianId).Description("Represents the unique identifier of the Nmédian who wrote the article.");
      descriptor.Field(x => x.Picture).Description("Represents the URL of the article's picture.");
      descriptor.Field(x => x.Published).Description("Represents the publication date of the article.");
      descriptor.Field(x => x.Title).Description("Represents the title of the article.");
      descriptor.Field(x => x.Updated).Description("Represents the latest update timestamp of the article.");
      
      descriptor.Field(x => x.Nmedian)
        .ResolveWith<Resolvers>(x => Resolvers.GetNmedian(default!, default!))
        .UseDbContext<NmediaContext>()
        .Description("Represents the Nmédian who wrote the article.");
    }

    private class Resolvers
    {
      public static Nmedian? GetNmedian(Article article, [ScopedService] NmediaContext dbContext)
      {
        return dbContext.Nmedians.SingleOrDefault(x => x.Id == article.NmedianId);
      }
    }
  }
}
