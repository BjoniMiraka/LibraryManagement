using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Validators
{
    // Custom validator to ensure date is not in the future
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime <= DateTime.Now;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} cannot be in the future.";
        }
    }
}

