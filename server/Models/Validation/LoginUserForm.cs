using System.ComponentModel.DataAnnotations;
using server.Services.Validation.Attributes;

namespace server.Models.Validation {
    public class LoginUserForm {
        [Required]
        public string UserName { get; set; }

        [StrongPassword]
        [Required]
        public string Password { get; set; }

/*
        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression("^[0-9]{6}$")]
        public string DualAuthCode {get; set; }
        */
    }
}