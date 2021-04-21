using System.ComponentModel.DataAnnotations;
using server.Models.Database;

namespace server.Models.Validation {
    public class WatchSentForm : BasicApiValidationForm {
        [Required]
        public string SendingAddress { get; set; }

        [Required]
        public string ReceivingAddress { get; set; }
    }
}