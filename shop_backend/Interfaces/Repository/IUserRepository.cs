using shop_backend.Models;

namespace shop_backend.Interfaces.Repository
{
    public interface IUserRepository
    {
        public List<User> SelectUsers();
        public User SelectUserById(int id);
        public void InsertUser(User user);
    }
}
