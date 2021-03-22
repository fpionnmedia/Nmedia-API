using System;

namespace Nmedia.Api.Application
{
  public interface IDateTimeOffset
  {
    DateTimeOffset Now { get; }
  }
}
