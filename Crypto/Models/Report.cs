using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public int AcountReporting { get; set; }
        public int AcountReported { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

    }
}
