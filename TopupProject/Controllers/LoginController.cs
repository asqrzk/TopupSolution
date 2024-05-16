using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TopupProject.Business.Interface;
using TopupProject.Helpers;
using TopupProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopupProject.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly string _Key;
        private readonly string _Issuer;
        private readonly string _Audience;
        private readonly JWTSettings _options;
        private readonly ICustomerService _service;

        public LoginController(IOptions<JWTSettings> options, ICustomerService service)
        {
            _options = options.Value;
            _Key = _options.Key;
            _Issuer = _options.Issuer;
            _Audience = _options.Audience;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            try
            {
                var authenticated = await _service.AuthenticateAsync(model.Username, model.Password);
                if (!authenticated) return Unauthorized();

                var token = LoginHelper.GenerateJwtToken(model, _options);
                return Ok(new { Token = token });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

