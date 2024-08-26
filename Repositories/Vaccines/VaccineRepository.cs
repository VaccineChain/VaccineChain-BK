using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Vaccines
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly ApplicationDbContext _context;

        public VaccineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteVaccine(Vaccine vaccine)
        {
            try
            {
                _context.Vaccines.Remove(vaccine);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error deleting Vaccine");
            }
        }

        public List<Vaccine> GetAll()
        {
            try
            {
                return _context.Vaccines.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Vaccine GetVaccineById(string id)
        {
            try
            {
                return _context.Vaccines.Find(id);
            }
            catch (Exception)   
            {
                throw new Exception("Error getting Vaccine");
            }
        }

        public List<Vaccine> GetVaccineByName(string name)
        {
            try
            {
                return _context.Vaccines
                               .Where(v => v.VaccineName.Contains(name))
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching Vaccines", ex);
            }
        }
        

        public void SaveVaccine(Vaccine v)
        {
            try
            {
                _context.Vaccines.Add(v);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateVaccine(Vaccine vaccine)
        {
            try
            {
                _context.Entry<Vaccine>(vaccine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw new Exception("Error updating Vaccine");
            }
        }
    }
}
