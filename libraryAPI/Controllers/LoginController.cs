using Library.Repositories.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Library.Services.Services;
using Library.Repositories.Repositories;
using Library.Services.Models;
using Microsoft.AspNetCore.Authorization;

namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController :ControllerBase
    {
        private readonly ILoginService _loginService;

        [ActivatorUtilitiesConstructor]
        public LoginController(LibraryContext libraryContext,IConfiguration configuration)
        {
            _loginService = new LoginService(new LoginRepository(configuration,libraryContext));
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserLogin userLogin)
        {
            var response = _loginService.Login(userLogin);
            if(response==null) 
                return NotFound("user not found");
            
            return Ok(response);
        }
    }
}
