using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Crypto.Models;

namespace Crypto.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }

    public class WalletModel
    {
        public IEnumerable<string> AcceptedCurrency { get; set; }
        public Wallet Wally { get; set; }
    }
}
