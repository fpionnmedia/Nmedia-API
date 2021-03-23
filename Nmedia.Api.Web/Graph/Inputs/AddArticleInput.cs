#nullable enable
using Nmedia.Domain.Articles;
using System;

namespace Nmedia.Api.Web.Graph.Inputs
{
  /// <summary>
  /// TODO: validation
  /// </summary>
  public record AddArticleInput(
    Category[]? Categories,
    string? Content,
    Guid? NmedianId,
    string? Picture,
    DateTime? Published,
    string Title
  );
}
