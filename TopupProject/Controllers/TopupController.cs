using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopupProject.Business.Interface;
using TopupProject.Entities;
using TopupProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopupProject.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TopupController : Controller
    {
        private readonly ITopUpService _topUpService;
        private readonly IBalanceService _balanceService;
        private readonly decimal[] options = new decimal[] { 5, 10, 20, 30, 50, 75, 100 };

        public TopupController(ITopUpService topUpService, IBalanceService balanceService)
        {
            _topUpService = topUpService;
            _balanceService = balanceService;
        }

        [HttpPost]
        public async Task<IActionResult> TopUp([FromBody] TopupRequest request)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null) return BadRequest();
                decimal balance = await _balanceService.GetBalanceAsync(username);
                decimal amount = options[request.OptionId - 1];
                decimal approvedamount = await _topUpService.GetApprovedAmount(username, amount, request);

                if (balance < approvedamount)
                    return BadRequest("Insufficient balance");

                await _balanceService.DebitBalanceAsync(approvedamount, username);
                bool success = await _topUpService.PerformTopUpAsync(request, approvedamount);

                return success ? Ok(new { Balance = balance - approvedamount }) : BadRequest();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("options")]
        public IActionResult GetTopUpOptions()
        {
            return Ok(options);
        }
    }
}

