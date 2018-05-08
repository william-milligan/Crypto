using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Models;

namespace Crypto.Models
{
    public class EFTransactions : ITransactions
    {
        private ApplicationDbContext context;
        private CurrentUser us = new CurrentUser();


        public EFTransactions(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Transaction> Transactions => context.Transactions;

        public Transaction GetTransaction(int transactionId)
        {
            Transaction t = null;
            while (true)
            {
                foreach (Transaction f in Transactions)
                    if (f.TransactionId == transactionId)
                    {
                        return f;
                    }
                   
                        transactionId++;

            }
            return null;
        }

        public void SaveTransaction(Transaction trans)
        {

            if(trans.TransactionId == 0)
            {
                context.Transactions.Add(trans);
            }
            else
            {
                Transaction dbEntry = context.Transactions
                    .FirstOrDefault(t => t.TransactionId == trans.TransactionId);
                if(dbEntry != null)
                {
                    dbEntry.UserName = trans.UserName;
                    dbEntry.Wallet = trans.Wallet;
                    dbEntry.AcountId = trans.AcountId;
                    dbEntry.Amount = trans.Amount;
                    dbEntry.AmountWanted = trans.AmountWanted;
                    dbEntry.CurrencyType = trans.CurrencyType;
                    dbEntry.CurrrencyWanted = trans.CurrrencyWanted;
                    dbEntry.TransactionDescription = trans.TransactionDescription;
                    dbEntry.TransactionInfo = trans.TransactionInfo;
                    dbEntry.TransactionTitle = trans.TransactionTitle;
                }
            }
            context.SaveChanges();
        }
    }
}
