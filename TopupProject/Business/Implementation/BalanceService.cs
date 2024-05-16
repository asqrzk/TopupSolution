using System;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TopupProject.Business.Interface;
using TopupProject.Helpers;
using TopupProject.Models;

namespace TopupProject.Business.Implementation
{
	public class BalanceService : IBalanceService
	{
        private readonly HttpClient _httpClient;
        private readonly URLSettings _options;

        public BalanceService(IHttpClientFactory httpClientFactory, IOptions<URLSettings> options )
		{
            _httpClient = httpClientFactory.CreateClient();
            _options = options.Value;
		}

        public async Task<bool> DebitBalanceAsync(decimal amount, string username)
        {
            try
            {
                var response = await _httpClient.PostAsync(_options.BalanceAPIUrl + username + "&amount=" + amount, null);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<decimal> GetBalanceAsync(string username)
        {
            try
            {
                var response = await _httpClient.GetAsync(_options.BalanceAPIUrl + username);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var account = JsonSerializer.Deserialize<AccountModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (account == null)
                    throw new InvalidOperationException("Account does not exist - BS201");

                return account.Balance;
            }
            catch(Exception) { throw; }
        }
    }
}

