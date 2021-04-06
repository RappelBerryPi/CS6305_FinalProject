using System.ComponentModel.DataAnnotations;
using server.Models.Database;
using server.Services.Validation.Attributes;

namespace server.Models.Validation {
    public class NewUserForm {
        [UserNameIsUnique]
        [Required]
        [RegularExpression(UserInfo.USER_NAME_REGEX)]
        public string UserName { get; set; }

        [Required]
        [StrongPassword]
        public string Password { get; set; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


    }

}