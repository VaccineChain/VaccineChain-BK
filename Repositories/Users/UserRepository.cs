using Microsoft.EntityFrameworkCore;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Users
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error creating user");
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email == email);
            }
            catch (Exception)
            {
                throw new Exception("Error getting user");
            }
        }
    }
}
