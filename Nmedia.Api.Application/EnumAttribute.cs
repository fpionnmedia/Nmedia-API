#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Nmedia.Api.Application
{
  public class EnumAttribute : ValidationAttribute
  {
    private readonly Type _type;

    public EnumAttribute(Type type)
    {
      _type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override bool IsValid(object? value)
    {
      if (value == null)
      {
        return true;
      }
      else if (value is IEnumerable<string> names)
      {
        return names.All(name => IsValid(name));
      }
      else if (value is string name)
      {
        return IsValid(name);
      }

      return false;
    }

    private bool IsValid(string name)
    {
      return Enum.TryParse(_type, name, out object value) && Enum.IsDefined(_type, value);
    }
  }
}
