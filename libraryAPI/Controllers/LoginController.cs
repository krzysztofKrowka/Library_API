using Library.Repositories.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Library.Services.Services;
using Library.Repositories.Repositories;
using Library.Services.Models;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions.Common;

namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController :ControllerBase
    {
        private readonly ILoginService _loginService;
        
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            var response = _loginService.Login(userLogin);
            if(response==null) 
                return NotFound("user not found");
            
            return Ok(response);
        }
    }
}
