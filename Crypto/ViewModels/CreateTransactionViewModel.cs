using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Models;
using Crypto.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Crypto.ViewModels
{
    public class CreateTransactionViewModel
    {
        [StringLength(45, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter a valid title")]
        public string TransactionTitle { get; set; }

        [StringLength(120, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter a valid description")]
        public string TransactionDescription { get; set; }
        [Required(ErrorMessage = "Please enter a valid amount")]
        public int Amount { get; set; }

        [StringLength(35, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the currency you want to offer")]
        public string CurrencyType { get; set; }

        [StringLength(35, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the currency type you wish to recieve")]
        public string CurrrencyWanted { get; set; }

        public string TransactionInfo { get; set; }
        [Required(ErrorMessage ="Please enter the amount you wish to revcieve")]
        public int AmountWanted { get; set; }

        [StringLength(45, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter a Wallet")]
        public string Wallet { get; set; }

        public IEnumerable<Wallet> TransactionWallets;

        public IEnumerable<String> SupportedWallets;



    }
}
