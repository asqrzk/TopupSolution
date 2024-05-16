using System.ComponentModel.DataAnnotations;
namespace TopupProject.Entities
{
    public class Beneficiary
    {
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "Nickname cannot be longer than 20 characters.")]
        public required string Nickname { get; set; }

        public virtual ICollection<Topup> Topups { get; set; } = new List<Topup>();
    }
}








