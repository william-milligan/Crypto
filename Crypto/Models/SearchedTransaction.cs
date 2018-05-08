using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class SearchedTransaction : ISearchedTransactions
    {
        public IEnumerable<Transaction> SearchedTransactions { get; set; }
    }
}
