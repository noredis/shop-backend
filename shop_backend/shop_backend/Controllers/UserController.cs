using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using shop_backend.Interfaces.Service;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            List<User> users = _userService.FindUser();
            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _userService.FindUserById(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public IResult RegisterUser([FromBody] RegisterUserRequestDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }

            User userModel = userDto.RegisterDtoToUser();
            string passwordConfirm = userDto.PasswordConfirm;

            IResult status = _userService.RegisterUser(userModel, passwordConfirm, Url);

            return status;
        }
    }
}
