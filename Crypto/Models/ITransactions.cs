using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public interface ITransactions
    {
        IEnumerable<Transaction> Transactions { get; }
        Transaction GetTransaction(int id);
        void SaveTransaction(Transaction tran);
    }
}
