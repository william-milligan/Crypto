using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crypto.Models
{
    public class Transaction
    {
        
        public string UserName { get; set; }

        public string TransactionTitle { get; set; }
        [StringLength(35, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter a valid title")]
        public string TransactionDescription { get; set; }
        public string Wallet { get; set; }
        public int Amount { get; set; }
        public Boolean Active { get; set; } = true;
        public int AcountId { get; set; }
        public string CurrencyType { get; set; }
        public string CurrrencyWanted { get; set; }
        public int TransactionId { get; set; }
        public string TransactionInfo { get; set; }
        public int AmountWanted { get; set; }

    }
}
