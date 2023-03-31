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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository loginRepository)
        {
            _userRepository = loginRepository;
        }
        public string Login(UserLogin userLogin)
        {
            
            var user = new User
            {
                Username = userLogin.Username,
                Password = userLogin.Password
            };
            
            return _userRepository.Login(user);
        
        }
        public async Task<User> Register(UserRegister userRegister)
        {
            var user = new User
            {
                Username= userRegister.Username,
                Password = userRegister.Password,
                Role = userRegister.Role,
                Id = Guid.NewGuid()
            };
            return await _userRepository.Register(user);
        }
    }
}
