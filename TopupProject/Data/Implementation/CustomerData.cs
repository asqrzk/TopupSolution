using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TopupProject.Data.Interface;
using TopupProject.Entities;

namespace TopupProject.Data.Implementation
{
	public class CustomerData : ICustomerData
	{
        private readonly TopupContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly string CacheKey = "CustomerBeneficiaries";

        public CustomerData(TopupContext context, IMemoryCache cache)
		{
			_context = context;
            _memoryCache = cache;
		}

        public async Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary, string username)
        {
            try
            {
                var customer = await GetCustomerByUsernameAsync(username);
                if (customer == null) throw new Exception("Customer Not Found - CD101");
                if (customer.Beneficiaries.Count >= 5) throw new Exception("Maximum 5 beneficiaries are allowed");
                customer.Beneficiaries.Add(beneficiary);
                await _context.SaveChangesAsync();
                _memoryCache.Remove(CacheKey);
                return beneficiary;
            }
            catch (Exception) { throw; }
        }

        public async Task<Customer?> GetCustomerByUsernameAsync(string username)
        {
            try
            {
                return await _context.Customers
                    .Where(w => w.Username == username).FirstOrDefaultAsync();
            }
            catch(Exception) { throw; }
        }

        public async Task<Customer?> GetCustomerWithBeneficiariesByUsernameAsync(string username)
        {
            try
            {
                var customer = await _memoryCache.GetOrCreateAsync(CacheKey, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                    return await _context.Customers
                        .Where(w => w.Username == username).Include(i => i.Beneficiaries).FirstOrDefaultAsync();
                });
                return customer;
            }
            catch (Exception) { throw; }
        }

        public async Task<Customer?> GetCompleteCustomerDataAsync(string username)
        {
            try
            {
                var now = DateTime.Now;
                var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                var firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);
                return await _context.Customers
                    .Where(w => w.Username == username)
                        .Include(i => i.Beneficiaries)
                            .ThenInclude(b => b.Topups.Where(t => t.Date >= firstDayOfMonth && t.Date < firstDayOfNextMonth))
                                .FirstOrDefaultAsync();
            }
            catch (Exception) { throw; }
        }
    }
}

