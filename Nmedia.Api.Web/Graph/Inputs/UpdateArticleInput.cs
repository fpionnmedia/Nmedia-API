#nullable enable
using Nmedia.Domain.Articles;
using System;

namespace Nmedia.Api.Web.Graph.Inputs
{
  /// <summary>
  /// TODO: validation
  /// </summary>
  public record UpdateArticleInput(
    Category[]? Categories,
    string? Content,
    Guid Id,
    Guid? NmedianId,
    string? Picture,
    DateTime? Published,
    string Title
  );
}
