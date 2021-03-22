﻿#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nmedia.Api.Application.Exceptions;

namespace Nmedia.Api.Web.Filters
{
  public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      if (context.Exception is ApiException exception)
      {
        context.ExceptionHandled = true;

        if (exception.Value == null)
        {
          context.Result = new StatusCodeResult((int)exception.StatusCode);
        }
        else
        {
          context.Result = new JsonResult(exception.Value)
          {
            StatusCode = (int)exception.StatusCode
          };
        }
      }
    }

  }
}
