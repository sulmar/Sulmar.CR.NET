using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sulmar.CR.NET.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var rpt = new ReportDocument();
            rpt.Load(@"Reports\JedenWierszArtykulParagraf.rpt");

            if (rpt.IsLoaded)
            {
                CrystalReportsViewer1.ViewerCore.ReportSource = rpt;
            }
        }
    }
}
