using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Models;
using Crypto.ViewModels;

namespace Crypto.Models.ViewModels
{
    public class TransactionsViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public SearchForm SearchForma { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}
