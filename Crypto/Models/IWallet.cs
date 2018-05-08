using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public interface IWallet
    {
        IEnumerable<Wallet> Wallets {get;}

        void SaveWallet(Wallet wallet);
    }
}
