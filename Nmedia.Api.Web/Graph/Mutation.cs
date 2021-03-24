#nullable enable
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Nmedia.Api.Application;
using Nmedia.Api.Persistence.Npgsql;
using Nmedia.Api.Web.Graph.Inputs;
using Nmedia.Api.Web.Graph.Payloads;
using Nmedia.Domain.Articles;
using Nmedia.Domain.Nmedians;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nmedia.Api.Web.Graph
{
  public class Mutation
  {
    [UseDbContext(typeof(NmediaContext))]
    public async Task<CreateArticlePayload> CreateArticleAsync(
      CreateArticleInput input,
      [Service] IDateTimeOffset dateTimeOffset,
      [Service] IGuid guid,
      [Service] ITopicEventSender sender,
      [ScopedService] NmediaContext dbContext,
      CancellationToken cancellationToken
    )
    {
      Nmedian? nmedian = null;
      if (input.NmedianId.HasValue)
      {
        nmedian = await dbContext.Nmedians.SingleOrDefaultAsync(
          x => x.Uuid == input.NmedianId.Value,
          cancellationToken
        ) ?? throw new ArgumentException($"The Nmédian (ID={input.NmedianId}) could not be found.", nameof(input));
      }

      var entity = new Article
      {
        Categories = input.Categories,
        Content = input.Content,
        Created = dateTimeOffset.Now,
        Nmedian = nmedian,
        NmedianId = nmedian?.Id,
        Picture = input.Picture,
        Published = input.Published,
        Title = input.Title,
        Uuid = guid.NewGuid()
      };

      dbContext.Articles.Add(entity);
      await dbContext.SaveChangesAsync(cancellationToken);

      await sender.SendAsync(nameof(Subscription.OnArticleSaved), entity, cancellationToken);

      return new CreateArticlePayload(entity);
    }

    [UseDbContext(typeof(NmediaContext))]
    public async Task<DeleteArticlePayload> DeleteArticleAsync(
      DeleteArticleInput input,
      [ScopedService] NmediaContext dbContext,
      CancellationToken cancellationToken
    )
    {
      Article entity = await dbContext.Articles
        .SingleOrDefaultAsync(x => x.Uuid == input.Id, cancellationToken)
        ?? throw new ArgumentException($"The Article (ID={input.Id}) could not be found.", nameof(input));

      dbContext.Articles.Remove(entity);
      await dbContext.SaveChangesAsync(cancellationToken);

      return new DeleteArticlePayload(entity);
    }

      [UseDbContext(typeof(NmediaContext))]
    public async Task<UpdateArticlePayload> UpdateArticleAsync(
      UpdateArticleInput input,
      [Service] IDateTimeOffset dateTimeOffset,
      [Service] ITopicEventSender sender,
      [ScopedService] NmediaContext dbContext,
      CancellationToken cancellationToken
    )
    {
      Nmedian? nmedian = null;
      if (input.NmedianId.HasValue)
      {
        nmedian = await dbContext.Nmedians.SingleOrDefaultAsync(
          x => x.Uuid == input.NmedianId.Value,
          cancellationToken
        ) ?? throw new ArgumentException($"The Nmédian (ID={input.NmedianId}) could not be found.", nameof(input));
      }

      Article entity = await dbContext.Articles
        .SingleOrDefaultAsync(x => x.Uuid == input.Id, cancellationToken)
        ?? throw new ArgumentException($"The Article (ID={input.Id}) could not be found.", nameof(input));

      entity.Categories = input.Categories;
      entity.Content = input.Content;
      entity.Nmedian = nmedian;
      entity.NmedianId = nmedian?.Id;
      entity.Picture = input.Picture;
      entity.Published = input.Published;
      entity.Title = input.Title;
      entity.Updated = dateTimeOffset.Now;

      await dbContext.SaveChangesAsync(cancellationToken);

      await sender.SendAsync(nameof(Subscription.OnArticleSaved), entity, cancellationToken);

      return new UpdateArticlePayload(entity);
    }

    [UseDbContext(typeof(NmediaContext))]
    public async Task<CreateNmedianPayload> CreateNmedianAsync(
      CreateNmedianInput input,
      [Service] IDateTimeOffset dateTimeOffset,
      [Service] IGuid guid,
      [ScopedService] NmediaContext dbContext,
      CancellationToken cancellationToken
    )
    {
      var entity = new Nmedian
      {
        Age = input.Age,
        Created = dateTimeOffset.Now,
        IsActive = input.IsActive,
        Hired = input.Hired,
        HourlyRate = input.HourlyRate,
        JobTitle = input.JobTitle,
        Name = input.Name,
        Picture = input.Picture,
        Slug = input.Slug,
        Uuid = guid.NewGuid()
      };

      dbContext.Nmedians.Add(entity);
      await dbContext.SaveChangesAsync(cancellationToken);

      return new CreateNmedianPayload(entity);
    }
  }
}
