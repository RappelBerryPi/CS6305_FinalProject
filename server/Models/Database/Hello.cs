using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Database {
    public class Hello {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Message { get; set; }
        public string Header { get; set; }
    }

}