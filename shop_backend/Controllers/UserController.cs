using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Dtos.RefreshToken;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepo, IUserService userService, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _userService = userService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            List<User> users = _userRepo.SelectUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("user/{id}")]
        //[Authorize]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _userRepo.SelectUserById(id);

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

        [HttpPost]
        [Route("login")]
        public IActionResult AuthorizeUser([FromBody] LogInUserDto logInUserDto)
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

        [HttpPost]
        [Route("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LogInResponceDto responceDto = _tokenService.RefreshAccessToken(refreshTokenDto.RefreshToken);

            if (responceDto == null)
            {
                return Unauthorized("The user was not found");
            }
            else
            {
                return Ok(responceDto);
            }
        }
    }
}
