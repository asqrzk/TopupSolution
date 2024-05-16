using System;
namespace TopupProject.Models
{
	public class AccountModel
	{
        public required string Username { get; set; }

        public decimal Balance { get; set; }

        public bool IsActive { get; set; }
    }
}

