using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Models
{
    public interface IReport
    {
        IEnumerable<Report> Reports { get; }

        void SaveReport(Report report);
    }
}
