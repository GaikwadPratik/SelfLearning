using LogUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketReportingWinSvc
{
    public partial class StockMarketReportingWinSvc : ServiceBase
    {
        public StockMarketReportingWinSvc()
        {
            InitializeComponent();
        }

        public void ObDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ApplicationLog.Instance.WriteInfo(String.Format("{0} started at '{1}'", GetType().Name, DateTime.Now));
                if (StockMarketRunnerStatus.StockRunnerCheck())
                {
                    StockMarketRunner runApp = new StockMarketRunner();
                    bool bRunStatus = runApp.RunApplication();
                    if (bRunStatus)
                        ApplicationLog.Instance.WriteInfo(String.Format("{0} successfully completed at '{1}'", GetType().Name, DateTime.Now));
                    else
                        ApplicationLog.Instance.WriteWarning(String.Format("{0} unsuccessfully completed at '{1}'", GetType().Name, DateTime.Now));
                    StockMarketRunnerStatus.WriteStatus();
                }
                else
                    ApplicationLog.Instance.WriteInfo(String.Format("{0} not ran at '{1}'", GetType().Name, DateTime.Now));

                ProjectInstaller projectInstaller = new ProjectInstaller();

                ServiceController serviceController = new ServiceController(projectInstaller.ServiceName);
                if (serviceController.Status.Equals(ServiceControllerStatus.Running)
                    || serviceController.Status.Equals(ServiceControllerStatus.StartPending))
                {
                    ApplicationLog.Instance.WriteInfo(String.Format("Stopping the service automatically at '{0}'", DateTime.Now));
                    this.Stop();
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                ApplicationLog.Instance.WriteInfo(String.Format("{0} stopped at '{1}'", GetType().Name, DateTime.Now));
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }
    }
}
