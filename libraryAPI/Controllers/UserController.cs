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
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService loginService)
        {
            _userService = loginService;
        }
        

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            
            var response = _userService.Login(userLogin);
            
            if(response==null) 
                return NotFound("user not found");
            
            return Ok(response);
        
        }
        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {

            var response = await _userService.Register(userRegister);

            return Created("User Registered",response);

        }
    }
}
