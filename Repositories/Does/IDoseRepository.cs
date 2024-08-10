using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Does
{
    public interface IDoseRepository
    {
        List<Dose> GetAll();
        Dose SaveDose(Dose d);
        Dose GetDose(int id);
        void DeleteDose(Dose dose);
        void UpdateDose(Dose dose);
    }
}
