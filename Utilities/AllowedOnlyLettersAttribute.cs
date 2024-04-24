using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Utilities
{
	public class AllowedOnlyLettersAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null)
			{
				string name = value.ToString().Replace(" ", "");

				if (name.All(Char.IsLetter))
				{
					return ValidationResult.Success;
				}
			}
			
			return new ValidationResult(ErrorMessage);
		}
	}
}
