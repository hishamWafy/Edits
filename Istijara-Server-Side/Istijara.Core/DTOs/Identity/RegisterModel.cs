using Istijara.Core.DTOs.Identity.CustomValidations;
using System.ComponentModel.DataAnnotations;


namespace Istijara.Core.DTOs.Identity
{

    public class RegisterModel
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailOrPhone]
        public string EmailOrPhone { get; set; }
        [Required, MaxLength(256)]
        public string Password { get; set; }
    }
}
