using Microsoft.AspNetCore.Mvc;

namespace BalanceProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private static List<Account> Accounts = new List<Account>()
    {
        new Account(){ Username="admin", Balance = 2000, IsActive = true},
        new Account(){ Username="asqrzk", Balance = 1000, IsActive = false}
    };

    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Accounts")]
    public List<Account> Get()
    {
        return Accounts;
    }

    [HttpGet("user")]
    public Account GetAccount([FromQuery] string username)
    {
        return Accounts.Where(w => w.Username == username).FirstOrDefault();
    }

    [HttpPost("user")]
    public decimal Topup([FromQuery]string username, [FromQuery]decimal amount)
    {
        var account = Accounts.Where(w => w.Username == username).FirstOrDefault();
        account.Balance -= amount;
        return account.Balance;
    }
}

