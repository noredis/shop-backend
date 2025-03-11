using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.RefreshToken;
using shop_backend.Dtos.User;
using shop_backend.Interfaces.Service;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        
        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IResult AuthorizeUser([FromBody] LogInUserDto logInUserDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }

            IResult status = _userService.Authorize(logInUserDto);

            return status;
        }

        [HttpPost]
        [Route("refresh")]
        public IResult RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }

            IResult status = _tokenService.RefreshAccessToken(refreshTokenDto.RefreshToken);

            return status;
        }
    }
}
