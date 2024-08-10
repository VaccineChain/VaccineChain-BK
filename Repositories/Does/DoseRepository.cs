using Microsoft.EntityFrameworkCore;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Does
{
    public class DoseRepository : IDoseRepository
    {
        private readonly ApplicationDbContext _context;
        public DoseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeleteDose(Dose dose)
        {
            try
            {
                _context.Doses.Remove(dose);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error deleting dose");
            }
        }

        public List<Dose> GetAll()
        {
            try
            {
                return _context.Doses.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dose GetDose(int id)
        {
            try
            {
                return _context.Doses.FirstOrDefault(u => u.DoseNumber == id);
            }
            catch (Exception)
            {
                throw new Exception("Error getting Dose");
            }
        }

        public Dose SaveDose(Dose d)
        {
            try
            {
                _context.Doses.Add(d);
                _context.SaveChanges();
                return d;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDose(Dose dose)
        {
            try
            {
                _context.Entry<Dose>(dose).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error updating dose");

            }
        }
    }
}
