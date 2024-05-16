using System;
using TopupProject.Entities;
using TopupProject.Models;

namespace TopupProject.Business.Interface
{
    public interface ITopUpService
    {
        Task<decimal> GetApprovedAmount(string username, decimal amount, TopupRequest topup);
        Task<bool> PerformTopUpAsync(TopupRequest request, decimal amount);
    }
}

