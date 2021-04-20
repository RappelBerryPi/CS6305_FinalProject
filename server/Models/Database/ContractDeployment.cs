using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Database {
    public class ContractDeployment {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string ByteCode { get; set; }
        public string ServerUrl { get; set; }
        public string DeploymentAddress { get; set; }
    }
}