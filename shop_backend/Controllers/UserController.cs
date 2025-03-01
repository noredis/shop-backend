using Microsoft.AspNetCore.Mvc;

using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public IActionResult GetUsers()
        {
            List<User> users = _userRepo.SelectUsers();
            //if (_userService.CheckNotFound(users))
            //{
            //    return NotFound("There are no registered users");
            //}
            //else
            //{
            //    return Ok(users);
            //}
            return Ok(users);
        }

        [HttpGet]
        [Route("user/{id}")]
        [Authorize]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _userRepo.SelectUserById(id);

            //if (_userService.CheckNotFound(user))
            //{
            //    return NotFound("Account with this id does not exist");
            //}
            //else
            //{
            //    return Ok(user);
            //}

            return Ok(user);
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

            int statusCode = _userService.Create(userModel, passwordConfirm);
            
            if (statusCode != 201)
            {
                return StatusCode(statusCode);
            }
            else
            {
                return CreatedAtAction(nameof(GetUserById), new { id = userModel.Id }, UserMappers.FromUser(userModel));
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult AuthorizeUser(LogInUserDto logInUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LogInResponceDto responceDto = _userService.Authorize(logInUserDto);

            if (responceDto != null)
            {
                return Ok(responceDto);
            }
            else
            {
                return Unauthorized("The user was not found");
            }
        }
    }
}
