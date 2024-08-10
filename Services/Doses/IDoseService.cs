using vaccine_chain_bk.DTO.Dose;

namespace vaccine_chain_bk.Services.Doses
{
    public interface IDoseService
    {
        List<DoseDto> GetAll();

        DoseDto GetById(int id);

        DoseDto CreateDose(CreateDoseDto createDoseDto);

        DoseDto UpdateDose(int id, UpdateDoseDto updateDoseDto);

        string DeleteDose(int id);
    }
}
