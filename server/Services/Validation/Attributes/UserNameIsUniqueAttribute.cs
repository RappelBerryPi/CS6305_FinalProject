using System.ComponentModel.DataAnnotations;
using server.Models.Database;
using System.Linq;

namespace server.Services.Validation.Attributes {
    public class UserNameIsUniqueAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            using (DefaultContext context = (DefaultContext) validationContext.GetService(typeof(DefaultContext))) {
                var userName = (string) validationContext.ObjectInstance;
                var anyMatch = context.UserInfos.Any(u => u.UserName.ToLower().Equals(userName.ToLower()));
                if (anyMatch) {
                    return new ValidationResult($"The username ${userName} already exists. Please choose a different one.");
                }
                return ValidationResult.Success;
            }
        }
    }
}