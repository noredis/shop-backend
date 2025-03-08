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
