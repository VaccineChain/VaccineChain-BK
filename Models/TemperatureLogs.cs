using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.Models
{
    public class TemperatureLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemperatureId { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public DateTime Timestamp { get; set; }
        public EStatus Status { get; set; }

    }
}
