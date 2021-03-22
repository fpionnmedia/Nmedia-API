#nullable enable
using Nmedia.Api.Application;
using System;

namespace Nmedia.Api.Infrastructure
{
  public class DateTimeOffsetService : IDateTimeOffset
  {
    public DateTimeOffset Now => DateTimeOffset.Now;
  }
}
