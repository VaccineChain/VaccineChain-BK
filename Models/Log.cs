using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        public double? Value { get; set; }

        public string? Unit { get; set; }

        public DateTime? Timestamp { get; set; }

        public EStatus? Status { get; set; }

        public string VaccineId { get; set; }

        [ForeignKey("VaccineId")]
        public virtual Vaccine Vaccine { get; set; }

        public string DeviceId { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }

}
