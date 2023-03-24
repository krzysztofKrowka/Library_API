using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Models
{        
    public class BaseEnitty
    {   
        public Guid ID { get; set; }
        public bool IsDeleted { get; set; }
    }   
}