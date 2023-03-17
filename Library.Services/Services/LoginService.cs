using Library.Repositories.Interfaces;
using Library.Repositories.Models;
using Library.Services.Interfaces;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Repositories.Repositories;
namespace Library.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public string Login(UserLogin userLogin)
        {
            User user = new User
            {
                Username = userLogin.Username,
                Password = userLogin.Password
            };
            return _loginRepository.Login(user);
        }
    }
}
