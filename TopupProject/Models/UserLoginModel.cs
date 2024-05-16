using System;
using System.ComponentModel.DataAnnotations;

namespace TopupProject.Models
{
	public class UserLoginModel
	{
        [StringLength(10, ErrorMessage = "Username cannot be longer than 10 characters.")]
        public required string Username { get; set; }

        [StringLength(10, ErrorMessage = "Password cannot be longer than 10 characters.")]
        public required string Password { get; set; }
    }
}

