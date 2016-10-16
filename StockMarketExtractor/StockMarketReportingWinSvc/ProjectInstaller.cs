using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace StockMarketReportingWinSvc
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void StockMarketReportingWinSvcInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController sc = new ServiceController(ServiceName);
            sc.Start();
        }
    }
}
