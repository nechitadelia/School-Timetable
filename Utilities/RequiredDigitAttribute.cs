using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Utilities
{
    public class RequiredDigitAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (name.Any(Char.IsDigit))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
