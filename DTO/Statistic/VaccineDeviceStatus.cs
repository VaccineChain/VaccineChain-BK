using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Statistic
{
    public class VaccineDeviceStatus
    {
        public string VaccineId { get; set; }
        public string VaccineName { get; set; }
        public int NumberOfDevices { get; set; }
        public EStatus Status { get; set; }
    }

}
