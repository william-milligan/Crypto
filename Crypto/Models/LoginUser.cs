using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class LoginUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int LoginUserId { get; set; }
    }
}
