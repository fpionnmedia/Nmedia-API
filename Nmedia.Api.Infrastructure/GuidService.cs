#nullable enable
using Nmedia.Api.Application;
using System;

namespace Nmedia.Api.Infrastructure
{
  public class GuidService : IGuid
  {
    public Guid NewGuid()
    {
      return Guid.NewGuid();
    }
  }
}
