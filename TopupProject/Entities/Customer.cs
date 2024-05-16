using System;
using System.ComponentModel.DataAnnotations;

namespace TopupProject.Entities
{
	public class Customer
	{
        public int Id { get; set; }

        [StringLength(10, ErrorMessage = "Username cannot be longer than 10 characters.")]
        public required string Username { get; set; }

        [StringLength(10, ErrorMessage = "Password cannot be longer than 10 characters.")]
        public required string Password { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
    }
}

