using System.ComponentModel.DataAnnotations;
using server.Services.Validation.Attributes;

namespace server.Models.Validation {
    public class LoginUserForm {
        [Required]
        public string UserName { get; set; }

        [StrongPassword]
        [Required]
        public string Password { get; set; }
    }
}