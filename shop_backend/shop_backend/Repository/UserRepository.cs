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

        public void InsertUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User SelectUserById(int id)
        {
            return _context.User.Find(id);
        }

        public List<User> SelectUsers()
        {
            return _context.User.ToList();
        }

        public bool FindUserByEmail(string email)
        {
            var user = _context.User.Where(u => u.Email == email);

            if (user.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
