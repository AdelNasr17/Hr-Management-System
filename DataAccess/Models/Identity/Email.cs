using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Identity
{
    public class Email
    {
        public string To { get; set; }
        public string Body { get; set; } 
        public string Subject { get; set; }
    }
}
