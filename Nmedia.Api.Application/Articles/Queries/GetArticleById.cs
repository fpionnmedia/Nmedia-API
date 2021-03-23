#nullable enable
using MediatR;
using Nmedia.Api.Application.Articles.Models;
using System;

namespace Nmedia.Api.Application.Articles.Queries
{
  public class GetArticleById : IRequest<ArticleModel>
  {
    public GetArticleById(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
