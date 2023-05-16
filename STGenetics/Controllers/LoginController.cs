using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        private readonly ILogin _loginRepo;

        public LoginController(ILogin loginRepo)
        {
            _loginRepo = loginRepo;
        }

        // POST: LoginController/Create
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Post([FromBody] Login loginPost)
        {
            try
            {
                var respone = await _loginRepo.LoginDB(loginPost);
                if (respone != null)
                {
                    return Ok(respone);
                }
                else
                {
                    return Unauthorized();
                }
                
            }
            catch (Exception ex)
            {
                //log error
                if (ex.Message == "Sequence contains no elements")
                {
                    return Unauthorized();
                }
                return StatusCode(500, ex.Message);
            }
        }


    }
}
