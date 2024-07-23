using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vaccine_chain_bk.Models
{
    public class Vaccines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]         
        public int VaccineId { get; set; }
        public string VaccineName { get; set; }
        public string Manufacturer { get; set; }
        public string BatchNumber { get; set; }
        public string ExpirationDate { get; set; }
        public virtual ICollection<TemperatureLogs>? TemperatureLogs { get; set; }
    }
}
