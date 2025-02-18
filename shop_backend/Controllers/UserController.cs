using Microsoft.AspNetCore.Mvc;
using shop_backend.Data;
using shop_backend.Models;
using shop_backend.Dtos.User;
using shop_backend.Mappers;

namespace shop_backend.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/users")]
        public IActionResult GetUsers()
        {
            List<User> users = _context.User.ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("/user/{id}")]
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
        [Route("/register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequestDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User userModel = userDto.RegisterDtoToUser();

            // Password confirmation match check
            if (userModel.Password.Equals(userModel.PasswordConfirm))
            {
                _context.User.Add(userModel);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUserbyId), new { id = userModel.Id }, userModel);
            }
            else
            {
                return BadRequest("The password confirmation does not match");
            }
        }
    }
}
