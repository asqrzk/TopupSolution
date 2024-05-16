using System;
namespace TopupProject.Business.Interface
{
	public interface ICustomerService
	{
		Task<bool> AuthenticateAsync(string username, string password);
	}
}

