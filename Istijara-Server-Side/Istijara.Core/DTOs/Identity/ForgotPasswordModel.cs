using System.ComponentModel.DataAnnotations;

namespace Istijara.Core.DTOs.Identity
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? ClientUri { get; set; }
    }
}
