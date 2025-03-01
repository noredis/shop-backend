using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface IUserService
    {
        public bool CheckNotFound(List<User> users);
        public bool CheckNotFound(User user);
        public bool SearchForEmail(string email);
        public bool ValidatePassword(string password);
        public void HashPassword(string password, string confirmation, out string encPaswword, out string encConfirmation);
        public bool ConfirmPassword(string encPassword, string encConfirmation);
        public int Create(User userModel, string passwordConfirm);
    }
}
