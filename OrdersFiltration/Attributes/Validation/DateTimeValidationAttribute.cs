using System.ComponentModel.DataAnnotations;

namespace OrdersFiltration.Attributes.Validation
{

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateTimeValidationAttribute : ValidationAttribute
    {
		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			DateTime date = (DateTime)value;

			if (date == default(DateTime) || date < DateTime.Now)
				return new ValidationResult("Неверно задано время");

			return ValidationResult.Success;
		}
	}
}