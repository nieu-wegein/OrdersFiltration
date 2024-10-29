using OrdersFiltration.Attributes.Validation;
using OrdersFiltration.Enums;


namespace OrdersFiltration.Models
{
	public class InputModel
	{
		[DistrictValidation]
		public District District { get; set; }

		[DateTimeValidation]
		public DateTime DateTime { get; set; }

	}
}
