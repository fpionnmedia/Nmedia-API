#nullable enable
using MediatR;
using System;

namespace Nmedia.Api.Application.Articles.Commands
{
  public class DeleteArticle : IRequest
  {
    public DeleteArticle(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; }
  }
}
