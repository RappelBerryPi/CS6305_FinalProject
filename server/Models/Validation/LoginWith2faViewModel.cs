using System.ComponentModel.DataAnnotations;

namespace server.Models.Validation {
    public class LoginWith2faViewModel {
        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression("^[0-9]{6}$")]
        public string DualAuthCode {get; set; }

    }
}