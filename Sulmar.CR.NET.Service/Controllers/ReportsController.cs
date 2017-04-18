using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sulmar.CR.NET.Service.Controllers
{
    public class ReportsController : ApiController
    {
        public IHttpActionResult Get(string id)
        {
            var rpt = new ReportDocument();
            rpt.Load(@"Reports\JedenWierszArtykulParagraf.rpt");

            var stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rpt.Close();
            rpt.Dispose();

            return new AttachmentActionResult("test.pdf", stream, "application/pdf");
        }
    }
}
