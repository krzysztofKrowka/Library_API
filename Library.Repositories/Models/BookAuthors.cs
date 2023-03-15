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
        public Guid ID { get; set; }// = Guid.NewGuid();
        public Guid Author_ID { get; set; }
        public Guid Book_ID { get; set; }
    }
}
