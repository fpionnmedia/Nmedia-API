#nullable enable
using MediatR;
using Nmedia.Api.Application.Articles.Models;
using System;

namespace Nmedia.Api.Application.Articles.Commands
{
  public class SaveArticle : IRequest<ArticleModel>
  {
    public SaveArticle(SaveArticleModel model, Guid? id = null)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
      Id = id;
    }

    public Guid? Id { get; }
    public SaveArticleModel Model { get; }
  }
}
