using System.ComponentModel.DataAnnotations;
using server.Models.Database;

namespace server.Models.Validation {
    public class BasicApiValidationForm {
        [Required]
        [RegularExpression(UserInfo.USER_NAME_REGEX)]
        public string UserName { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression("^[0-9]{6}$")]
        public string DualAuthCode {get; set; }
    }
}