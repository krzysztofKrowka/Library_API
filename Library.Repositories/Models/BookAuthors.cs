using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Models
{
    public class BookAuthors
    {
        public int ID { get; set; }
        public int Author_ID { get; set; }
        public int Book_ID { get; set; }
    }
}
