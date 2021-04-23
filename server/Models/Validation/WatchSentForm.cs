using System.ComponentModel.DataAnnotations;

namespace server.Models.Validation {
    public class WatchSentForm : BasicApiValidationForm {
        [Required]
        public string SendingAddress { get; set; }

        [Required]
        public string ReceivingAddress { get; set; }
    }
}