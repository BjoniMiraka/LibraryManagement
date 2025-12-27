using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Validators
{
    // Custom validator to ensure minimum age requirement
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
            ErrorMessage = $"You must be at least {_minimumAge} years old.";
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime dateOfBirth)
            {
                var age = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
                {
                    age--;
                }
                return age >= _minimumAge;
            }
            return true;
        }
    }
}

