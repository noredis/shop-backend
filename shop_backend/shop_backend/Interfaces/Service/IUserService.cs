using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.User;
using shop_backend.Models;
using shop_backend.Validation;

namespace shop_backend.Interfaces.Service
{
    public interface IUserService
    {
        public bool ValidatePassword(string password);
        public void HashRegisterPassword(string password, string confirmation, out string encPaswword, out string encConfirmation);
        public bool ConfirmPassword(string encPassword, string encConfirmation);
        public void GenerateToken(User user, out string accessToken, out string refreshToken);
        public Task<Result<UserResponce>> RegisterUser(User userModel, string passwordConfirm, IUrlHelper urlHelper, string role);
        public Result<LogInResponceDto> AuthorizeUser(LogInUserDto logInUserDto);
        public Result<List<User>> GetUsers();
        public Result<User?> FindUserById(int id);
    }
}
