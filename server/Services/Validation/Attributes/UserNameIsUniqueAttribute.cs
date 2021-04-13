using System.ComponentModel.DataAnnotations;
using server.Models.Database;
using System.Linq;

namespace server.Services.Validation.Attributes {
    public class UserNameIsUniqueAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            DefaultContext context = (DefaultContext) validationContext.GetService(typeof(DefaultContext));
            var userName = (string) value;
            if (userName == null) { return ValidationResult.Success; }
            var anyMatch = context.Users.Any(u => u.UserName.ToLower().Equals(userName.ToLower()));
            if (anyMatch) {
                return new ValidationResult($"The username ${userName} already exists. Please choose a different one.");
            }
            return ValidationResult.Success;
        }
    }
}