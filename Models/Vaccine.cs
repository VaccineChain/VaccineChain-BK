using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vaccine_chain_bk.Models
{
    public class Vaccine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string VaccineId { get; set; }

        public string VaccineName { get; set; }
            
        public string Manufacturer { get; set; }

        public string BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<Log>? Logs { get; set; }

        public virtual ICollection<Dose>? Doses { get; set; }

    }
}
