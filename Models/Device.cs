using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.Models
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DeviceId { get; set; }

        public string Location { get; set; }

        public EType SensorType { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}
