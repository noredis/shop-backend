using shop_backend.Data;
using shop_backend.Interfaces.Repository;
using shop_backend.Models;

namespace shop_backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User FindUserById(int id)
        {
            return _context.User.Find(id);
        }

        public List<User> GetUsers()
        {
            return _context.User.ToList();
        }

        public void FindUserBySignInCredentials(string email, string password, out bool result, out User? user)
        {
            List<User> users = _context.User.Where(u => u.Email == email && u.Password == password).ToList();
                
            if (users.Count() > 0)
            {
                result = true;
                user = users[0];
            }
            else
            {
                result = false;
                user = null;
            }
        }

        public bool FindUserByEmail(string email)
        {
            var user = _context.User.Where(u => u.Email == email);

            if (user.Count() > 0)
                return true;
            else
                return false;
        }
    }
}
