﻿using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.User
{
    public class RegisterUserRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string FullName { get; set; } = String.Empty;
        [Required]
        [MinLength(6, ErrorMessage = "Password should be at least 6 characters length")]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string PasswordConfirm { get; set; } = String.Empty;
        [Required]
        public string Role { get; set; } = String.Empty;
    }
}
