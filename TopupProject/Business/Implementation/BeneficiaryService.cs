using System;
using TopupProject.Business.Interface;
using TopupProject.Data.Interface;
using TopupProject.Entities;

namespace TopupProject.Business.Implementation
{
	public class BeneficiaryService : IBeneficiaryService
	{
        private readonly IBeneficiaryData _data;
        private readonly ICustomerData _customer;

		public BeneficiaryService(IBeneficiaryData data, ICustomerData customer)
		{
            _data = data;
            _customer = customer;
		}

        public async Task<Beneficiary> AddBeneficiaryAsync(string nickname, string username)
        {
            try
            {
                var beneficiary = await _data.AddBeneficiaryAsync(nickname);
                return await _customer.AddBeneficiaryAsync(beneficiary, username);
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<Beneficiary>> GetBeneficiariesAsync(string username)
        {
            try
            {
                Customer? customer = await _customer.GetCustomerByUsernameAsync(username);
                if (customer == null) throw new Exception("Customer Not Found - BS102");
                return customer.Beneficiaries.ToList();
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<Beneficiary>> GetCustomerWithBeneficiariesByUsernameAsync(string username)
        {
            try
            {
                Customer? customer = await _customer.GetCustomerWithBeneficiariesByUsernameAsync(username);
                if (customer == null) throw new Exception("Customer Not Found - BS102");
                return customer.Beneficiaries.ToList();
            }
            catch (Exception) { throw; }
        }
    }
}

