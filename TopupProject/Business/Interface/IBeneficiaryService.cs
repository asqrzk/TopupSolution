using System;
using TopupProject.Entities;

namespace TopupProject.Business.Interface
{
    public interface IBeneficiaryService
    {
        Task<Beneficiary> AddBeneficiaryAsync(string nickname, string username);
        Task<IEnumerable<Beneficiary>> GetCustomerWithBeneficiariesByUsernameAsync(string username);
        Task<IEnumerable<Beneficiary>> GetBeneficiariesAsync(string username);
    }
}

