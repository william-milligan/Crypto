using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crypto.Models
{
    public class Wallet
    {
        public int WalletID { get; set; }
        [Required]
        public string WalletName { get; set; }
        public int UserID { get; set; }
        [Required]
        public string CurrencyType { get; set; }
    }
}
