using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using server.Services.Validation.Attributes;

namespace server.Models.Database {
    public class UserInfo : IdentityUser<long> {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [PersonalData]
        public override long Id { get; set; }

        [ProtectedPersonalData]
        public string FirstName { get; set; }

        [ProtectedPersonalData]
        public string LastName { get; set; }

        [Required]
        [ProtectedPersonalData]
        public string BlockchainAddress { get; set; }

        [EmailAddress]
        [ProtectedPersonalData]
        public override string Email { get; set; }

        [UserNameIsUnique]
        [ProtectedPersonalData]
        public override string UserName { get; set; }

        [ProtectedPersonalData]
        [RegularExpression("^[a-zA-Z0-9]{16}$")]
        [StringLength(16,MinimumLength = 16)]
        public string DualAuthenticationSecretKey {get; set;}

        public const string USER_NAME_REGEX = "^[A-Za-z][A-Za-z0-9_]{7,59}$";

    }

}