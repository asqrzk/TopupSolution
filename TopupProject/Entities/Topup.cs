using System;
namespace TopupProject.Entities
{
    public class Topup
    {
        public int Id { get; set; }

        public int BeneficiaryId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }

}

