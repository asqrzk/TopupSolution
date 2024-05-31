using System;
using TopupProject.Entities;

namespace TopupProject.Data.Interface
{
	public interface IBeneficiaryData
	{
        Task<Beneficiary> AddBeneficiaryAsync(string nickname);
        Task<Beneficiary> AddTopupAsync(Topup topup);
        //Task<IEnumerable<Beneficiary>> GetBeneficiariesAsync(int userid);
    }
}

