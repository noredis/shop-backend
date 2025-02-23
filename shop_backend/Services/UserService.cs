using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Models;

using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace shop_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public bool CheckNotFound(List<User> users)
        {
            return users == null;
        }

        public bool CheckNotFound(User user)
        {
            return user == null;
        }

        public bool ConfirmPassword(string encPassword, string encConfirmation)
        {
            return encPassword.Equals(encConfirmation);
        }

        public void HashPassword(string password, string confirmation, out string encPassword, out string encConfirmation)
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

        public bool SearchForEmail(string email)
        {
            var user = _userRepo.SelectUsers().Where(u => u.Email == email).ToList();
            bool emailFound = user.Exists(u => u.Email == email);
            return emailFound;
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
    }
}
