using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public class EFReport : IReport
    {
        ApplicationDbContext context;

        public EFReport(ApplicationDbContext cnt)
        {
            context = cnt;
        }

        public IEnumerable<Report> Reports => context.Reports;

        public void SaveReport(Report report)
        {

            context.Reports.Add(report);

            context.SaveChanges();
        }
    }
}

