using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class SearchForm
    {
        public string CurrencyType { get; set; }
        public int? MinAmount { get; set; }
        public int? MaxAmount { get; set; }
        public bool Active { get; set; } = true;
        public string UserName { get; set; } = "";

        public SearchForm getSearchForm()
        {
            SearchForm s = new SearchForm();
            return s;
        }

        public IEnumerable<String> SupportedWallets;
    }
}
