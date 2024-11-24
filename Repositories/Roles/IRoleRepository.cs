using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Roles
{
    public interface IRoleRepository
    {
        Role GetRoleByName(string name);
    }
}
