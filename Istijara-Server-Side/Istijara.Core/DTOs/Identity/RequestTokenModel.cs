using Istijara.Core.DTOs.Identity.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Istijara.Core.DTOs.Identity
{
    public class RequestTokenModel
    {
        [Required, EmailOrPhone]
        public string EmailOrPhone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
