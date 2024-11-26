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

        public User GetUserById(Guid id)
        {
            try
            {
                return _context.Users
                    .Include(u => u.Role)
                    .AsNoTracking()
                    .FirstOrDefault(u => u.UserId == id);
            }
            catch (Exception)
            {
                throw new Exception("Error getting user");
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Entry<User>(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error updating user");
            }
        }
    }
}
