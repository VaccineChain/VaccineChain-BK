using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Role GetRoleByName(string name)
        {
            try
            {
                return _context.Roles.FirstOrDefault(r => r.Name == name);
            }
            catch (Exception)
            {
                throw new Exception("Error getting role");
            }
        }
    }
}
