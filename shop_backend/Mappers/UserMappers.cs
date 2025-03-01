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
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };
        }

        public static UserResponce FromUser(User user)
        {
            return new UserResponce
            {
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
