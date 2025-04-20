using shop_backend.Models;

namespace shop_backend.Interfaces.Repository
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User FindUserById(int id);
        public void AddUser(User user);
        public bool FindUserByEmail(string email);
        public void FindUserBySignInCredentials(string email, string password, out bool result, out User? user);
    }
}
