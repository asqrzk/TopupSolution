using System;
using TopupProject.Entities;

namespace TopupProject.Data.Interface
{
	public interface ITopupData
	{
        Task<bool> PerformTopUpAsync(Topup request);
    }
}

