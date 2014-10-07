using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RequestPasswordReset
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }
    }
}