using System;
namespace TopupProject.Business.Interface
{
    public interface IBalanceService
    {
        Task<decimal> GetBalanceAsync(string username);
        Task<bool> DebitBalanceAsync(decimal amount, string username);
    }
}

