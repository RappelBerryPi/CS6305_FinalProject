using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Models.Database;
using server.Services.Validation.Attributes;

namespace server.Models.Validation {
    public class NewWatchForm : BasicApiValidationForm {
        [Required]
        public string WatchName { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string LongDescription { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

    }

}