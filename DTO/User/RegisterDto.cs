using System.ComponentModel.DataAnnotations;
using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.User
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The email can not empty!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        public EAccount Status { get; set; } = EAccount.Active;
    }
}
