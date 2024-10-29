using OrdersFiltration.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrdersFiltration.Attributes.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DistrictValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			if ((District)value == District.None)
				return new ValidationResult("Район не найден");

			return ValidationResult.Success;
		}
	}
}
