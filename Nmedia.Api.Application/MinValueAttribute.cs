#nullable enable
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Api.Application
{
  public class MinValueAttribute : ValidationAttribute
  {
    private readonly double _minValue;

    public MinValueAttribute(double minValue)
    {
      _minValue = minValue;
    }

    public override bool IsValid(object? value)
    {
      if (value == null)
      {
        return true;
      }
      else if (value is decimal number)
      {
        return number >= (decimal)_minValue;
      }

      return false;
    }
  }
}
