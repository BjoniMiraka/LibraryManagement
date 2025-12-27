using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Validators
{
    // Custom validator for ISBN format (ISBN-10 or ISBN-13)
    public class ISBNAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return true; // Let Required attribute handle null/empty

            string isbn = value.ToString()!.Replace("-", "").Replace(" ", "");

            // Check if it's ISBN-10 or ISBN-13
            if (isbn.Length == 10)
            {
                return IsValidISBN10(isbn);
            }
            else if (isbn.Length == 13)
            {
                return IsValidISBN13(isbn);
            }

            return false;
        }

        private bool IsValidISBN10(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += (isbn[i] - '0') * (10 - i);
            }

            char lastChar = isbn[9];
            sum += lastChar == 'X' ? 10 : (lastChar - '0');

            return sum % 11 == 0;
        }

        private bool IsValidISBN13(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{13}$"))
                return false;

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = isbn[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int checkDigit = (10 - (sum % 10)) % 10;
            return checkDigit == (isbn[12] - '0');
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a valid ISBN-10 or ISBN-13 format.";
        }
    }
}

