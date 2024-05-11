using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Utilities
{
	public class AllowedOnlyIntAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null)
			{
				string number = value.ToString();
				if (float.TryParse(number, out _))
				{
					if (float.Parse(number) == int.Parse(number))
					{
						return ValidationResult.Success;
					}
				}
			}

			return new ValidationResult(ErrorMessage);
		}
	}
}
