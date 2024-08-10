using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Dose
{
    public class UpdateDoseDto : CreateDoseDto
    {
        public int DoseNumber { get; set; }

    }
}
