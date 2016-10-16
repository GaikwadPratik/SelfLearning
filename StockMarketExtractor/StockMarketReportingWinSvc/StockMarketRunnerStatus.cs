using LogUtils;
using SerializationExtensionUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketReportingWinSvc
{
    public class StockMarketRunnerStatus
    {
        static String requestFilePath = @"C:\PortfoliExcels\files\StockRequest.xml";
        static String strStatusFile = @"C:\PortfoliExcels\files\Status.xml";
        static String rawRequestFilePath = @"C:\PortfoliExcels\files\RawRequestInfo.txt";
        public DateTime LastRan { get; set; }
        public DayOfWeek LastRanDay { get; set; }
        public DateTime LastModifiedRequestFile { get; set; }
        public DateTime LastModifiedRawFile { get; set; }
        public static bool StockRunnerCheck()
        {
            bool bRtnVal = true;
            try
            {
                StockMarketRunnerStatus obj = new StockMarketRunnerStatus();
                if (File.Exists(strStatusFile))
                {
                    obj = SerializationHelper.LoadXML<StockMarketRunnerStatus>(strStatusFile);
                    if (
                        ((DateTime.Today.DayOfWeek.Equals(DayOfWeek.Monday) && DateTime.Now.TimeOfDay.Hours < 16) //Don't run on Monday before 4pm if ran on Friday after 4 pm..
                            || (DateTime.Today.DayOfWeek.Equals(DayOfWeek.Saturday) || DateTime.Today.DayOfWeek.Equals(DayOfWeek.Sunday))
                        && (obj.LastRanDay.Equals(DayOfWeek.Friday) && obj.LastRan.TimeOfDay.Hours > 16)) // Don't run on Saturday or Sunday if ran on Friday after 4 pm.
                        || ((obj.LastRan.TimeOfDay.Hours > 16 && DateTime.Now.TimeOfDay.Hours < 16) //Don't run if last run is yestarday after 4 pm and current time is less than 4pm.
                        && (obj.LastRanDay.Equals(DateTime.Now.AddDays(-1).DayOfWeek)))
                        || (obj.LastRan.AddHours(12) > DateTime.Now) //Don't run if ran less than 12 hrs.
                        )
                        bRtnVal = false;
                }
                else
                    ApplicationLog.Instance.WriteWarning(String.Format("Status file not found at '{0}'", strStatusFile));
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bRtnVal;
        }

        public static void WriteStatus()
        {
            try
            {
                StockMarketRunnerStatus obj = new StockMarketRunnerStatus();
                obj.LastRan = DateTime.Now;
                obj.LastRanDay = DateTime.Now.DayOfWeek;
                obj.LastModifiedRequestFile = File.GetLastWriteTime(requestFilePath);
                obj.LastModifiedRawFile = File.GetLastWriteTime(rawRequestFilePath);
                SerializationHelper.WriteXML<StockMarketRunnerStatus>(obj, strStatusFile);
                ApplicationLog.Instance.WriteInfo(String.Format("Staus file modified at '{0}'", DateTime.Now));
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        public static bool CreateRequestFileBasedOnStatus()
        {
            bool bRtnVal = false;
            try
            {
                if (File.Exists(strStatusFile))
                {
                    StockMarketRunnerStatus obj = SerializationHelper.LoadXML<StockMarketRunnerStatus>(strStatusFile);
                    DateTime dtLastWritten = File.GetLastWriteTime(rawRequestFilePath);
                    if (!dtLastWritten.Equals(obj.LastModifiedRawFile))
                        bRtnVal = true;
                }
                else
                    bRtnVal = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bRtnVal;
        }
    }
}
