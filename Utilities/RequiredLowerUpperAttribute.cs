using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Utilities
{
    public class RequiredLowerUpperAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (name.Any(Char.IsLower) && name.Any(Char.IsUpper))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
