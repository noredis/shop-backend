using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

using shop_backend.Data;
using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using System.Text;
using Microsoft.EntityFrameworkCore.Query;
using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepo, IUserService userService)
        {
            _userRepo = userRepo;
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            List<User> users = _userRepo.SelectUsers();
            if (_userService.CheckNotFound(users))
            {
                return NotFound("There are no registered users");
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _userRepo.SelectUserById(id);
            
            if (_userService.CheckNotFound(user))
            {
                return NotFound("Account with this id does not exist");
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

            if (_userService.SearchForEmail(userModel.Email))
            {
                return BadRequest("Account with this email already exists");
            }

            if (!_userService.ValidatePassword(userModel.Password))
            {
                return BadRequest("Password must contain lowercase and uppercase latin letters and at least 1 digit and special symbol." +
                    " Password must not contain any spaces, tabs or newlines");
            }
            else
            {
                _userService.HashPassword(userModel.Password, passwordConfirm, out encPassword, out encConfirmation);
            }

            if (!_userService.ConfirmPassword(encPassword, encConfirmation))
            {
                return BadRequest("The password confirmation does not match");
            }
            else
            {
                userModel.Password = encPassword;
                _userRepo.InsertUser(userModel);
                return CreatedAtAction(nameof(GetUserById), new { id = userModel.Id }, userModel.ToUserResponceDto());
            }
        }
    }
}
