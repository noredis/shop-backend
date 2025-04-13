using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.User;
using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Mappers;
using shop_backend.Models;

using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace shop_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public bool ConfirmPassword(string encPassword, string encConfirmation)
        {
            return encPassword.Equals(encConfirmation);
        }

        public void HashLogInPassword(string password, out string encPassword)
        {
            MD5 md5 = MD5.Create();

            byte[] pwdByte = ASCIIEncoding.ASCII.GetBytes(password);

            pwdByte = md5.ComputeHash(pwdByte);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in pwdByte)
            {
                sb.Append(item.ToString("x2"));
            }

            encPassword = sb.ToString();
        }

        public void HashRegisterPassword(string password, string confirmation, out string encPassword, out string encConfirmation)
        {
            MD5 md5 = MD5.Create();

            byte[] pwdByte = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] confirmByte = ASCIIEncoding.ASCII.GetBytes(confirmation);

            pwdByte = md5.ComputeHash(pwdByte);
            confirmByte = md5.ComputeHash(confirmByte);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in pwdByte)
            {
                sb.Append(item.ToString("x2"));
            }

            encPassword = sb.ToString();

            sb.Clear();

            foreach (byte item in confirmByte)
            {
                sb.Append(item.ToString("x2"));
            }

            encConfirmation = sb.ToString();
        }

        public bool ValidatePassword(string password)
        {
            bool isValid = false;

            Regex uppercase = new Regex("[A-Z]");
            Regex lowercase = new Regex("[a-z]");
            Regex digit = new Regex("[0-9]");
            Regex specSymbol = new Regex("[\\$!\\^\\-+/@%&\\.\\*/\\(\\)?#\\[\\]\\{\\}:;,\"<>\\|~'`_№]");
            Regex tab = new Regex("\t");
            Regex newline = new Regex("\n");
            Regex space = new Regex("[ ]");

            Match hasUppercase = uppercase.Match(password);
            Match hasLowercase = lowercase.Match(password);
            Match hasDigit = digit.Match(password);
            Match hasSpecSymbol = specSymbol.Match(password);
            Match hasTab = tab.Match(password);
            Match hasNewline = newline.Match(password);
            Match hasSpace = space.Match(password);

            isValid = hasUppercase.Success &&
                hasLowercase.Success &&
                hasDigit.Success &&
                hasSpecSymbol.Success &&
                !hasTab.Success &&
                !hasNewline.Success &&
                !hasSpace.Success;

            return isValid;
        }

        public Results<Created<UserResponce>, BadRequest<string>> RegisterUser(User userModel, string passwordConfirm, IUrlHelper urlHelper)
        {
            string encPassword = "";
            string encConfirmation = "";

            if (_userRepo.FindUserByEmail(userModel.Email))
            {
                return TypedResults.BadRequest("Account with this email already exists");
            }

            if (!ValidatePassword(userModel.Password))
            {
                return TypedResults.BadRequest("Password must contain lowercase and uppercase latin letters and at least 1 digit and special symbol. " +
                    "Password must not contain any spaces, tabs or newlines");
            }
            else
            {
                HashRegisterPassword(userModel.Password, passwordConfirm, out encPassword, out encConfirmation);
            }

            if (!ConfirmPassword(encPassword, encConfirmation))
            {
                return TypedResults.BadRequest("The password confirmation does not match");
            }
            else
            {
                userModel.Password = encPassword;
                _userRepo.InsertUser(userModel);

                string? locationHeader = urlHelper.Action("GetUserById", "User", new {id = userModel.Id});

                return TypedResults.Created(locationHeader, UserMappers.FromUser(userModel));
            }
        }

        public Results<Ok<LogInResponceDto>, UnauthorizedHttpResult> AuthorizeUser(LogInUserDto logInUserDto)
        {
            string encPassword = string.Empty;
            string accessToken = string.Empty;
            string refreshToken = string.Empty;

            HashLogInPassword(logInUserDto.Password, out encPassword);

            List<User> registeredUsers = _userRepo.SelectUsers().ToList();
            User? currentUser = registeredUsers.Find(u => u.Password.Equals(encPassword) && u.Email == logInUserDto.Email);

            if (currentUser == null)
            {
                return TypedResults.Unauthorized();
            }
            else
            {
                _tokenService.CreateToken(currentUser, out accessToken, out refreshToken);
                return TypedResults.Ok(
                    new LogInResponceDto
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    });
            }
        }

        public List<User> FindUser()
        {
            List<User> users = _userRepo.SelectUsers();
            return users;
        }

        public User? FindUserById(int id)
        {
            User? user = _userRepo.SelectUserById(id);
            return user;
        }
    }
}
