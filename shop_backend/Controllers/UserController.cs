using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using shop_backend.Interfaces.Service;
using shop_backend.Dtos.RefreshToken;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            List<User> users = _userService.Find();
            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _userService.FindById(id);
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

            IResult status = _userService.Create(userModel, passwordConfirm, Url);

            return status;
        }
    }
}
