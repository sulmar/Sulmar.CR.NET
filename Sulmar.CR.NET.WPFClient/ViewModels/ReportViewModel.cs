using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulmar.CR.NET.WPFClient.ViewModels
{
    public class ReportViewModel
    {
        public ReportDocument Report { get; set; }

        public ReportViewModel()
        {
            LoadReport();

        }

        private void LoadReport()
        {
            var rpt = new ReportDocument();
            rpt.Load(@"Reports\JedenWierszArtykulParagraf.rpt");

            Report = rpt;
        }
    }
}
