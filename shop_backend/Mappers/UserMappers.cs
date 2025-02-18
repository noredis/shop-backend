using shop_backend.Models;
using shop_backend.Dtos.User;

namespace shop_backend.Mappers
{
    public static class UserMappers
    {
        public static User RegisterDtoToUser(this RegisterUserRequestDto userDto)
        {
            return new User
            {
                Email = userDto.Email,
                FullName = userDto.FullName,
                Password = userDto.Password,
                PasswordConfirm = userDto.PasswordConfirm,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };
        }
    }
}
