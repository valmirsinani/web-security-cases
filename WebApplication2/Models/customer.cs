using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class customer
    {
        public int customerId { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
    public class account
    {
        public int accountId { get; set; }
        public int customerId { get; set; }
        public customer customer { get; set; }
        public decimal balance { get; set; }
    }
}
