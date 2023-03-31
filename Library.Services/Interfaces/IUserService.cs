using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Repositories.Models;

namespace Library.Services.Interfaces
{
    public interface IUserService
    {
        string Login(UserLogin userLogin);
        Task<User> Register(UserRegister userRegister);
    }
}
