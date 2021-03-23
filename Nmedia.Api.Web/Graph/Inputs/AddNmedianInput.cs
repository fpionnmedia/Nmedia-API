#nullable enable
using Nmedia.Domain.Nmedians;
using System;

namespace Nmedia.Api.Web.Graph.Inputs
{
  /// <summary>
  /// TODO: validation
  /// </summary>
  public record AddNmedianInput(
    short? Age,
    DateTime? Hired,
    decimal? HourlyRate,
    bool IsActive,
    JobTitle? JobTitle,
    string Name,
    string? Picture,
    string? Slug
  );
}
