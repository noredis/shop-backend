using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.User;
using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Mappers;
using shop_backend.Models;
using shop_backend.Validation;

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

        public Result<UserResponce> RegisterUser(User userModel, string passwordConfirm, IUrlHelper urlHelper)
        {
            string encPassword = "";
            string encConfirmation = "";

            if (_userRepo.FindUserByEmail(userModel.Email))
            {
                return Result<UserResponce>.Failure(new Error("Account with this email already exists", 400));
            }

            if (!ValidatePassword(userModel.Password))
            {
                return Result<UserResponce>.Failure(new Error(
                    "Password must contain lowercase and uppercase latin letters and at least 1 digit and special symbol. " +
                    "Password must not contain any spaces, tabs or newlines",
                    400)
                );

            }
            else
            {
                HashRegisterPassword(userModel.Password, passwordConfirm, out encPassword, out encConfirmation);
            }

            if (!ConfirmPassword(encPassword, encConfirmation))
            {
                return Result<UserResponce>.Failure(new Error("The password confirmation does not match", 400));
            }
            else
            {
                userModel.Password = encPassword;
                _userRepo.AddUser(userModel);

                string? locationHeader = urlHelper.Action("GetUserById", "User", new { id = userModel.Id });

                return Result<UserResponce>.Success(UserMappers.FromUser(userModel), locationHeader);
            }
        }
        public Result<LogInResponceDto> AuthorizeUser(LogInUserDto logInUserDto)
        {
            string encPassword = string.Empty;
            string accessToken = string.Empty;
            string refreshToken = string.Empty;

            HashLogInPassword(logInUserDto.Password, out encPassword);

            bool isFound;
            User? currentUser;

            _userRepo.FindUserBySignInCredentials(logInUserDto.Email, encPassword, out isFound, out currentUser);

            if (!isFound)
            {
                return Result<LogInResponceDto>.Failure(new Error(String.Empty, 401));
            }
            else
            {
                _tokenService.GenerateToken(currentUser, out accessToken, out refreshToken);
                return Result<LogInResponceDto>.Success(
                    new LogInResponceDto
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    }
                );
            }
        }

        public Result<List<User>> GetUsers()
        {
            List<User> users = _userRepo.GetUsers();
            
            if (users.Count() < 0)
            {
                return Result<List<User>>.Failure(new Error("There are no registered users", 404));
            }
            else
            {
                return Result<List<User>>.Success(users);
            }
        }

        public Result<User?> FindUserById(int id)
        {
            User? user = _userRepo.FindUserById(id);

            if (user == null)
            {
                return Result<User?>.Failure(new Error("The user does not exist", 404));
            }
            else
            {
                return Result<User?>.Success(user);
            }
        }
    }
}
