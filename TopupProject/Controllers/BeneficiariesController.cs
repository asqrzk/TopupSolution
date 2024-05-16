using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopupProject.Business.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopupProject.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BeneficiariesController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;

        public BeneficiariesController(IBeneficiaryService beneficiaryService)
        {
            _beneficiaryService = beneficiaryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiary([FromBody] string nickname)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null) return BadRequest();
                var beneficiary = await _beneficiaryService.AddBeneficiaryAsync(nickname, username);
                return Ok(beneficiary);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public async Task<IActionResult> GetBeneficiaries()
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null) return BadRequest();
                var beneficiaries = await _beneficiaryService.GetCustomerWithBeneficiariesByUsernameAsync(username);
                return Ok(beneficiaries);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}

