using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Utilities
{
    public class RequiredNonalphanumericAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();

                if (!name.All(Char.IsLetterOrDigit))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
