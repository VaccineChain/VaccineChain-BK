using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vaccine_chain_bk.Models
{
    public class Doses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoseNumber { get; set; }
        public DateTime DateAdministered { get; set; }
        public string LocationAdministered { get; set; }
        public string Administrator {  get; set; }
    }
}
