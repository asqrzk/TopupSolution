using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TopupProject.Data.Interface;
using TopupProject.Entities;

namespace TopupProject.Data.Implementation
{
	public class BeneficiaryData : IBeneficiaryData
	{
        private readonly TopupContext _context;

        public BeneficiaryData(TopupContext context, IMemoryCache cache)
		{
            _context = context;
		}

        public async Task<Beneficiary> AddBeneficiaryAsync(string nickname)
        {
            try
            {
                var beneficiary = new Beneficiary { Nickname = nickname };
                await _context.Beneficiaries.AddAsync(beneficiary);
                await _context.SaveChangesAsync();
                return beneficiary;
            }
            catch(Exception) { throw; }
        }

        public async Task<Beneficiary> AddTopupAsync(Topup topup)
        {
            try
            {
                var beneficiary = await _context.Beneficiaries.Where(w => w.Id == topup.BeneficiaryId).FirstOrDefaultAsync();
                if (beneficiary == null) throw new Exception("Beneficiary Not Found - BD101");
                beneficiary.Topups.Add(topup);
                await _context.SaveChangesAsync();
                return beneficiary;
            }
            catch (Exception) { throw; }
        }

        //public async Task<IEnumerable<Beneficiary>> GetBeneficiariesAsync(int userid)
        //{
        //    return await _context.Beneficiaries.ToListAsync();
        //}
    }
}

