using System.ComponentModel.DataAnnotations;
using server.Models.Database;

namespace server.Models.Validation {
    public class WatchAssembledForm : BasicApiValidationForm {
        [Required]
        public string PhysicalAddress { get; set; }
    }
}