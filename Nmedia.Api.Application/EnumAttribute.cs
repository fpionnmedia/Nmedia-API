#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

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
      else if (value is string name)
      {
        return Enum.TryParse(_type, name, out object result) && Enum.IsDefined(_type, result);
      }

      return false;
    }
  }
}
