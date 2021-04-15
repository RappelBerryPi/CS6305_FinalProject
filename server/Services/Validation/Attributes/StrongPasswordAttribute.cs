using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace server.Services.Validation.Attributes {
    public class StrongPasswordAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            string password = (string) value;

            // password must be between 8 and 30 characters.
            if (password == null) {
                return new ValidationResult("Your password cannot be empty and is required");
            }
            var passwordMinLength = 8;
            var passwordMaxLength = 30;
            if (password.Length < passwordMinLength || password.Length > passwordMaxLength)  {
                return new ValidationResult($"Your password must be at least {passwordMinLength}, and no more than {passwordMaxLength} characters.");
            }

            // password must have at least 1 upper case character.
            const string BaseString = "Your password must contain at least 1 {0} character.";
            if (!password.Any(char.IsUpper)) {
                return new ValidationResult(String.Format(BaseString, "upper case"));
            }

            // password must have at least 1 lower case character.
            if (!password.Any(char.IsLower)) {
                return new ValidationResult(String.Format(BaseString, "lower case"));
            }

            // password must have at least 1 digit.
            if (!password.Any(char.IsDigit)) {
                return new ValidationResult(String.Format(BaseString, "digit"));
            }

            // password must have at least 1 special character.
            const string Special_Char_String = "!@#$%^&*?`~|";
            if (!password.Any(c => Special_Char_String.Contains(c))) {
                return new ValidationResult(String.Format(BaseString, "special") + $" Valid Special characters are '{Special_Char_String}'.");
            }

            // password cannot contain any whitespace.
            if (password.Any(char.IsWhiteSpace)) {
                return new ValidationResult("Your password cannot contain any spaces, tabs, or newlines.");
            }

            return ValidationResult.Success;
        }
    }
}