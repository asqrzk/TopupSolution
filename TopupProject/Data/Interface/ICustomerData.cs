using System;
using TopupProject.Entities;

namespace TopupProject.Data.Interface
{
	public interface ICustomerData
	{
        Task<Customer?> GetCustomerByUsernameAsync(string username);
        Task<Customer?> GetCustomerWithBeneficiariesByUsernameAsync(string username);
        Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary, string username);
        Task<Customer?> GetCompleteCustomerDataAsync(string username);
    }
}

