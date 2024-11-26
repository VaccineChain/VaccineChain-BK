using vaccine_chain_bk.DTO.User;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Users
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUserByEmail(string email);
        User GetUserById(Guid id);
        void UpdateUser(User user);
    }
}
