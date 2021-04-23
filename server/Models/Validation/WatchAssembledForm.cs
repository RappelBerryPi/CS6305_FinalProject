using System.ComponentModel.DataAnnotations;

namespace server.Models.Validation {
    public class WatchAssembledForm : BasicApiValidationForm {
        [Required]
        public string PhysicalAddress { get; set; }
    }
}