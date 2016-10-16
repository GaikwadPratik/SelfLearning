using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketReportingWinSvc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            StockMarketReportingWinSvc service = new StockMarketReportingWinSvc();
            service.ObDebug();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new StockMarketReportingWinSvc() 
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
