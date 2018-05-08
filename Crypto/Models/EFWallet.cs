using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class EFWallet : IWallet
    {
        private ApplicationDbContext context;

        public EFWallet(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Wallet> Wallets => context.Wallets;

        public void SaveWallet(Wallet wallet)
        {
            context.Add(wallet);
            context.SaveChanges();
        }
    }
}
