using System;
using TopupProject.Data.Interface;
using TopupProject.Entities;

namespace TopupProject.Data.Implementation
{
	public class TopupData : ITopupData
	{
        private readonly TopupContext _context;


		public TopupData(TopupContext context)
		{
            _context = context;
		}

        public async Task<bool> PerformTopUpAsync(Topup request)
        {
            try
            {
                request.Date = DateTime.Now;
                await _context.Topups.AddAsync(request);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception) { throw; }
        }
    }
}

