#nullable enable
using MediatR;
using Nmedia.Api.Application.Articles.Models;
using System.Collections.Generic;

namespace Nmedia.Api.Application.Articles.Queries
{
  public class GetAllArticles : IRequest<IEnumerable<ArticleItemModel>>
  {
  }
}
