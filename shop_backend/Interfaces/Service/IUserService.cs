using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.User;
using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface IUserService
    {
        public bool SearchForEmail(string email);
        public bool ValidatePassword(string password);
        public void HashPassword(string password, string confirmation, out string encPaswword, out string encConfirmation);
        public bool ConfirmPassword(string encPassword, string encConfirmation);
        public Results<Created<UserResponce>, BadRequest<string>> Create(User userModel, string passwordConfirm, IUrlHelper urlHelper);
        public Results<Ok<LogInResponceDto>, UnauthorizedHttpResult> Authorize(LogInUserDto logInUserDto);
        public List<User> Find();
        public User? FindById(int id);
    }
}
