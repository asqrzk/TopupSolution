using System;
using Azure.Core;
using TopupProject.Business.Interface;
using TopupProject.Data.Interface;
using TopupProject.Entities;
using TopupProject.Models;

namespace TopupProject.Business.Implementation
{
	public class TopUpService : ITopUpService
	{
        private readonly ITopupData _data;
        private readonly ICustomerData _customer;
        private readonly IBeneficiaryData _beneficiary;
        private readonly int _approvedMaxPerBeneficiary = 1000;
        private readonly int _unapprovedMaxPerBeneficiary = 500;
        private readonly int _maxPerCustomer = 3000;
        private readonly int _charge = 1;

        private readonly TopupContext _context;


        public TopUpService(ITopupData data, ICustomerData customer, IBeneficiaryData beneficiary, TopupContext context)
		{
            _data = data;
            _customer = customer;
            _beneficiary = beneficiary;
            _context = context;
		}

        public async Task<decimal> GetApprovedAmount(string username, decimal amount, TopupRequest request)
        {
            try
            {
                var customer = await _customer.GetCompleteCustomerDataAsync(username);
                if (customer == null) throw new Exception("Customer Not Found - TS101");
                decimal approvedAmount = ValidateAndGetApprovedAmount(customer, amount, request);
                return approvedAmount;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> PerformTopUpAsync(TopupRequest request, decimal amount)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Topup topup = new Topup() { BeneficiaryId = request.BeneficiaryId, Amount = amount };
                await _data.PerformTopUpAsync(topup);
                await _beneficiary.AddTopupAsync(topup);
                await transaction.CommitAsync();
                return true;
            }
            catch(Exception) {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private decimal ValidateAndGetApprovedAmount(Customer customer,decimal amount, TopupRequest request)
        {
            try
            {
                int maxPerBeneficiary = customer.IsActive ? _approvedMaxPerBeneficiary : _unapprovedMaxPerBeneficiary;
                decimal customerTotal = 0;
                decimal beneficiaryTotal = 0;
                foreach (Beneficiary beneficiary in customer.Beneficiaries)
                {
                    beneficiaryTotal = 0;
                    foreach (Topup topup in beneficiary.Topups)
                    {
                        customerTotal += topup.Amount;
                        if (beneficiary.Id == request.BeneficiaryId) beneficiaryTotal += topup.Amount;
                    }
                    if (beneficiaryTotal + amount > maxPerBeneficiary) throw new Exception("Topup limit per beneficiary exceeded");
                }
                if (customerTotal + amount > _maxPerCustomer) throw new Exception("Topup limit exceeded");
                return amount + _charge;
            }
            catch (Exception) { throw; }
        }
    }
}

