using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulmar.CR.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintTest();

            ExportOptionsTest();

            SelectionFormulaTest();

            SetLocationTest();

            GetParametersTest();

            SetRangeParameterTest();

            SetParametersTest();

            LoadAndExportReportTest();
        }

     

        private static void PrintTest()
        {
            var rpt = new ReportDocument();

            rpt.Load(@"Reports\JedenWierszArtykulParagraf.rpt");

            if (rpt.IsLoaded)
            {
                Console.WriteLine("Report was loaded.");

                // Print all pages to default printer
                rpt.PrintToPrinter(1, true, 0, 0);
            }
        }

        private static void ExportOptionsTest()
        {

            var rpt = new ReportDocument();

            rpt.Load(@"Reports\JedenWierszArtykulParagraf.rpt");

            if (rpt.IsLoaded)
            {
                Console.WriteLine("Report was loaded.");

                var formatOptions = ExportOptions.CreatePdfFormatOptions();
                formatOptions.CreateBookmarksFromGroupTree = true;

                ExportDestinationOptions destinationOptions = new DiskFileDestinationOptions()
                {
                    DiskFileName = "test.pdf"
                };


                ExportOptions exportOptions = new ExportOptions
                {
                    ExportDestinationType = ExportDestinationType.DiskFile,
                    ExportDestinationOptions = destinationOptions,

                    ExportFormatType = ExportFormatType.PortableDocFormat,
                    ExportFormatOptions = formatOptions,
                };

                rpt.Export(exportOptions);

                System.Diagnostics.Process.Start("report.pdf");
            }
        }

        private static void SelectionFormulaTest()
        {
            var beginDate = DateTime.Parse("2017-01-01");
            var endDate = DateTime.Parse("2017-06-01");

            var rpt = new ReportDocument();

            rpt.Load(@"Reports\ReportForPeriod.rpt");

            if (rpt.IsLoaded)
            {
                var periodRangeValue = new ParameterRangeValue { StartValue = beginDate, EndValue = endDate };

                rpt.RecordSelectionFormula = rpt.RecordSelectionFormula + " AND {Orzeczenia.JednostkaId} = 3";

                rpt.SetParameterValue("Okres", periodRangeValue);

                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, "SelectionFormulaTest.pdf");

                System.Diagnostics.Process.Start("ReportForPeriod.pdf");
            }


        }

        private static void LoadAndExportReportTest()
        {
            var rpt = new ReportDocument();


            rpt.Load(@"Reports\JedenWierszArtykulParagraf.rpt");

            if (rpt.IsLoaded)
            {
                Console.WriteLine("Report was loaded.");

                if (rpt.HasSavedData)
                {
                    rpt.Refresh();
                }

                rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "report.pdf");

                System.Diagnostics.Process.Start("report.pdf");
            }

            rpt.Close();

            rpt.Dispose();
        }

        private static void SetParametersTest()
        {
            var personId = 3;

            var rpt = new ReportDocument();

            rpt.Load(@"Reports\Orzeczenia.rpt");

            if (rpt.IsLoaded)
            {
                rpt.SetParameterValue("OsobaId", personId);

                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, "Orzeczenia.pdf");

                System.Diagnostics.Process.Start("Orzeczenia.pdf");
            }


        }

        private static void SetRangeParameterTest()
        {
            var beginDate = DateTime.Parse("2017-01-01");
            var endDate = DateTime.Parse("2017-06-01");

            var rpt = new ReportDocument();

            rpt.Load(@"Reports\ReportForPeriod.rpt");

            if (rpt.IsLoaded)
            {
                var periodRangeValue = new ParameterRangeValue { StartValue = beginDate, EndValue = endDate };

                rpt.SetParameterValue("Okres", periodRangeValue);

                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, "ReportForPeriod.pdf");

                System.Diagnostics.Process.Start("ReportForPeriod.pdf");
            }
        }


        private static void GetParametersTest()
        {
            var rpt = new ReportDocument();
            rpt.Load(@"Reports\ReportForPeriod.rpt");

            if (rpt.IsLoaded)
            {
                var fields = rpt.DataDefinition.ParameterFields
               .Cast<ParameterFieldDefinition>()
               .ToList();

                var parameterFieldsUsage = fields.Where(p => p.ParameterFieldUsage2.HasFlag(ParameterFieldUsage2.InUse));

                foreach (var field in parameterFieldsUsage)
                {

                    Console.WriteLine($"{field.ParameterFieldName} { field.PromptText} {field.ParameterValueKind} { field.ParameterType} {field.DiscreteOrRangeKind} ");

                }
            }


        }

        private static void SetLocationTest()
        {

            var connectionInfo = new ConnectionInfo
            {
                ServerName = @"DESKTOP-KT89MMD\SQLEXPRESS",
                DatabaseName = "CrystalReportsDb",
                IntegratedSecurity = true,
                // UserID = "",
                // Password = "",
               
            };


            var rpt = new ReportDocument();
            rpt.Load(@"Reports\ReportForPeriod.rpt");

            if (rpt.IsLoaded)
            {

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in rpt.Database.Tables)
                {
                    TableLogOnInfo logOnInfo = table.LogOnInfo;

                    logOnInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(logOnInfo);

                    Console.WriteLine($"{table.Name}");
                }


                rpt.ExportToDisk(ExportFormatType.CrystalReport, "SetLocationTest.rpt");

                //System.Diagnostics.Process.Start("SetLocationTest.pdf");
            }
        }
    }

}
