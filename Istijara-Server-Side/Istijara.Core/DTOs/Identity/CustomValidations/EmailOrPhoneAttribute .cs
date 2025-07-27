using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Istijara.Core.DTOs.Identity.CustomValidations
{
    public class EmailOrPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = value as string;

            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult("Email or phone number is required.");

            var isEmail = new EmailAddressAttribute().IsValid(input);
            var isPhone = Regex.IsMatch(input, @"^01[0-2,5]{1}[0-9]{8}$");

            if (!isEmail && !isPhone)
                return new ValidationResult("Please enter a valid email address or Egyptian phone number.");

            return ValidationResult.Success;
        }
    }
}
