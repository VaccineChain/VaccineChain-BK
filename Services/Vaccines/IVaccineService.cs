using vaccine_chain_bk.DTO.Vaccine;

namespace vaccine_chain_bk.Services.Vaccines
{
    public interface IVaccineService
    {
        List<VaccineDto> GetAll();

        VaccineDto GetById(string id);

        VaccineDto CreateVaccine(CreateVaccineDto createVaccineDto);

        VaccineDto UpdateVaccine(string id, UpdateVaccineDto updateVaccineDto);

        string DeleteVaccine(string id);
    }
}
