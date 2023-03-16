using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Models
{
    public class Librarian
    {
        public Guid LibrarianID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
