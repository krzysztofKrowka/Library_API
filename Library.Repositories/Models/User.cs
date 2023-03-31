using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}
