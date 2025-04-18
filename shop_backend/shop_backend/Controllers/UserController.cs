using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;
using shop_backend.Interfaces.Service;
using shop_backend.Validation;

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
        public IResult GetUsers()
        {
            Result<List<User>> requestResult = _userService.GetUsers();

            if (requestResult.IsSuccess)
            {
                return TypedResults.Ok(requestResult.Value);
            }
            else
            {
                return TypedResults.NotFound(requestResult.Error);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("user/{id}")]
        public IResult GetUserById([FromRoute] int id)
        {
            Result<User?> requestResult = _userService.FindUserById(id);

            if (requestResult.IsSuccess)
            {
                return TypedResults.Ok(requestResult.Value);
            }
            else
            {
                return TypedResults.NotFound(requestResult.Error);
            }
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

            Result<UserResponce> requestResult = _userService.RegisterUser(userModel, passwordConfirm, Url);

            if (requestResult.IsSuccess)
            {
                return TypedResults.Created(requestResult.Location, requestResult.Value);
            }
            else
            {
                return TypedResults.BadRequest(requestResult.Error);
            }
        }
    }
}
