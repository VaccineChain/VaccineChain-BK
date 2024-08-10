
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public EAccount Status { get; set; }

        public string? ProfilePicture { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

    }
}
