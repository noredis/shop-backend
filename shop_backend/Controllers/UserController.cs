using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

using shop_backend.Data;
using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using System.Text;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            List<User> users = _context.User.ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetUserbyId([FromRoute] int id)
        {
            var user = _context.User.Find(id);
            
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequestDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User userModel = userDto.RegisterDtoToUser();
            string passwordConfirm = userDto.PasswordConfirm;

            string encPassword = "";
            string encConfirmation = "";

            if (!ValidatePassword(userModel.Password))
            {
                return BadRequest("Password must contain lowercase and uppercase latin letters and at least 1 digit and special symbol." +
                    " Password must not contain any spaces, tabs or newlines");
            }
            else
            {
                HashPassword(userModel.Password, passwordConfirm, out encPassword, out encConfirmation);
            }

            if (!ConfirmPassword(encPassword, encConfirmation))
            {
                return BadRequest("The password confirmation does not match");
            }
            else
            {
                userModel.Password = encPassword;
                _context.User.Add(userModel);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUserbyId), new { id = userModel.Id }, userModel.ToUserResponceDto());
            }
        }

        private bool ValidatePassword(string password)
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

        private void HashPassword(string password, string confirmation, out string encPassword, out string encConfirmation)
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

        private bool ConfirmPassword(string encPassword, string encConfirmation)
        {
            return encPassword.Equals(encConfirmation);
        }
    }
}
