#nullable enable
using HotChocolate;
using HotChocolate.Types;
using Nmedia.Api.Persistence.Npgsql;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;
using System.Linq;

namespace Nmedia.Api.Web.Graph.Types
{
  public class NmedianType : ObjectType<Nmedian>
  {
    protected override void Configure(IObjectTypeDescriptor<Nmedian> descriptor)
    {
      descriptor.Description("Represents a member of the Nmédia team.");

      descriptor.Field(x => x.Id).Ignore();
      descriptor.Field(x => x.Uuid).Name("id")
        .Description("Represents the unique identifier of the Nmédian.");

      descriptor.Field(x => x.Age).Description("Represents the age in years of the Nmédian.");
      descriptor.Field(x => x.Created).Description("Represents the creation timestamp of the Nmédian.");
      descriptor.Field(x => x.Hired).Description("Represents the date when the Nmédian was hired.");
      descriptor.Field(x => x.HourlyRate).Description("Represents the hourly rate billed when the Nmédian works on a project.");
      descriptor.Field(x => x.IsActive).Description("Represents the activation status of the Nmédian.");
      descriptor.Field(x => x.JobTitle).Description("Represents the job title of the Nmédian.");
      descriptor.Field(x => x.Name).Description("Represents the name of the Nmédian.");
      descriptor.Field(x => x.Picture).Description("Represents the URL of the Nmédian's picture.");
      descriptor.Field(x => x.Slug).Description("Represents the unique URL segment of the Nmédian.");
      descriptor.Field(x => x.Updated).Description("Represents the latest update timestamp of the Nmédian.");

      descriptor.Field(x => x.Articles)
        .ResolveWith<Resolvers>(x => Resolvers.GetArticles(default!, default!))
        .UseDbContext<NmediaContext>()
        .Description("Represents the article list authored by the Nmédian.");
    }

    private class Resolvers
    {
      public static IQueryable<Article> GetArticles(Nmedian nmedian, [ScopedService] NmediaContext dbContext)
      {
        return dbContext.Articles.Where(x => x.NmedianId == nmedian.Id);
      }
    }
  }
}
