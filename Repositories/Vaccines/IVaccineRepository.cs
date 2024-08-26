using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Vaccines
{
    public interface IVaccineRepository
    {
        List<Vaccine> GetAll();

        void SaveVaccine(Vaccine vaccine);

        Vaccine GetVaccineById(string id);

        List<Vaccine> GetVaccineByName(string name);

        void DeleteVaccine(Vaccine vaccine);

        void UpdateVaccine(Vaccine vaccine);
    }
}
