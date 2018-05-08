using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public interface ISearchedTransactions
    {
        IEnumerable<Transaction> SearchedTransactions {get;set;} 
    }
}
