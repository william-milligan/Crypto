using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.ViewModels;
using Crypto.Models;

namespace Crypto.ViewModels
{
    public class SpecificTransactionViewModel
    {
        public Transaction Trans { get; set; }

        public User CurrentUser { get; set; }


    }
}
