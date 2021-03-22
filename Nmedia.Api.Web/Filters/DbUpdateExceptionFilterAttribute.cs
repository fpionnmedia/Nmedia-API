#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Net;

namespace Nmedia.Api.Web.Filters
{
  public class DbUpdateExceptionFilterAttribute : ExceptionFilterAttribute
  {
    private static readonly HashSet<string> _conflicts = new(new[]
    {
      "UQ_Nmedians_Slug"
    });

    public override void OnException(ExceptionContext context)
    {
      if (context.Exception is DbUpdateException exception
        && exception.InnerException is PostgresException pgException
        && pgException.ConstraintName != null)
      {
        if (_conflicts.Contains(pgException.ConstraintName))
        {
          Handle(context, HttpStatusCode.Conflict);
        }
      }
    }

    private static void Handle(ExceptionContext context, HttpStatusCode statusCode)
    {
      context.ExceptionHandled = true;
      context.Result = new StatusCodeResult((int)statusCode);
    }

  }
}
