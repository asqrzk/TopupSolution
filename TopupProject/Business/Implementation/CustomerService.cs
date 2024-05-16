using System;
using TopupProject.Business.Interface;
using TopupProject.Data.Interface;
using TopupProject.Entities;

namespace TopupProject.Business.Implementation
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerData _data;

		public CustomerService(ICustomerData data)
		{
			_data = data;
		}

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            try
            {
                Customer? customer = await _data.GetCustomerByUsernameAsync(username) ?? throw new Exception("Customer Not Found - BS101");
                return password == customer.Password;

            }
            catch (Exception) { throw; }
        }
    }
}

