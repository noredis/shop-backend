﻿using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.RefreshToken;
using shop_backend.Dtos.User;
using shop_backend.Interfaces.Service;
using shop_backend.Validation;

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

            Result<LogInResponceDto> requestResult = _userService.AuthorizeUser(logInUserDto).Result;
            
            if (requestResult.IsSuccess)
            {
                return TypedResults.Ok(requestResult.Value);
            }
            else
            {
                return TypedResults.Unauthorized();
            }
        }

        [HttpPost]
        [Route("refresh")]
        public IResult RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }

            Result<LogInResponceDto> requestResult = _tokenService.RefreshAccessToken(refreshTokenDto.RefreshToken);

            if (requestResult.IsSuccess)
            {
                return TypedResults.Ok(requestResult.Value);
            }
            else
            {
                return TypedResults.Unauthorized();
            }
        }
    }
}
