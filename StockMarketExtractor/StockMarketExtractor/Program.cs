using EmailUtils;
using LogUtils;
using Microsoft.Office.Interop.Excel;
using SerializationExtensionUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketExtractor
{
    class Program
    {
        static String requestFilePath = @"files\StockRequest.xml";
        static String rowRequestFilePath = @"files\RawRequestInfo.txt";
        static String PortfoliExcelPath = @"C:\ConsolePortfoliExcels\";
        static readonly String excelFileName = DateTime.Today.Date.ToString("MM-dd-yyyy");
        static void Main(string[] args)
        {
            List<string> strSyms = new List<string>()
            {
                "AARTIDRUGS","ALKEM","BAJAJ-AUTO","BAJAJFINSV","BAJAJHLDNG","BAJFINANCE","BANKBARODA","BANKINDIA","BATAINDIA","BEPL","CAIRN","CEATLTD","CUB","DISHTV","ESCORTS",
                "ESSAROIL","GICHSGFIN","GLENMARK","GMRINFRA","GOLDENTOBC","HARRMALAYA","HDFC","HDFCBANK","IBREALEST","IBULHSGFIN","IBWSL","ICICIBANK","IDBI","IDEA","IDFC","IFCI",
                "INDUSINDBK","INFY","IVC","JPASSOCIAT","KAMATHOTEL","KAVVERITEL","L&TFH","LICHSGFIN","LT","MALUPAPER","MANGLMCEM","MCLEODRUSS","MRPL","NEYVELILIG","NIITTECH","NITINFIRE",
                "PBAINFRA","PEL","PFC","PIRPHYTO","PNB","POWERGRID","RANASUG","RCF","RCOM","RELCAPITAL","RELIANCE","RIIL","ROLTA","RPOWER","RTNINFRA","SADBHAV","SBIN","SELAN","SHYAMTEL",
                "SITICABLE","SOBHA","STEL","SUMMITSEC","SUNPHARMA","SYNDIBANK","SYNGENE","TATACOMM","TATAPOWER","TATASTEEL","TIMETECHNO","TVTODAY","UCOBANK","ULTRACEMCO","VIJAYABANK",
                "VOLTAS","XLENERGY","ZEEL","ZEELEARN","ZEEMEDIA"
            };
            StringBuilder sb = new StringBuilder();

            foreach (string str in strSyms)
            {
                HttpWebRequest req = HttpWebRequest.Create(string.Format("http://www.nseindia.com/live_market/dynaContent/live_watch/get_quote/ajaxCompanySearch.jsp?search={0}", str)) as HttpWebRequest;
                req.Accept = "*/*";
                req.Host = "nseindia.com";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0";
                req.Method = "GET";
                req.KeepAlive = true;
                String strResult = String.Empty;
                using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
                {
                    using (Stream s = res.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            strResult = sr.ReadToEnd().ToString().Trim();
                            int ind = strResult.IndexOf("<b >") + 4;
                            int ind1 = strResult.IndexOf("<span class='symbol'>");
                            int length = ind1 - ind;
                            strResult = strResult.Substring(ind, length);
                            sb.AppendFormat("{0}{1}", strResult, Environment.NewLine);
                            Console.WriteLine(strResult);
                        }
                    }
                }
            }
            File.WriteAllText(@"c:\1.txt", sb.ToString());
            Console.ReadLine();
            //if (!File.Exists(requestFilePath))
            //{
            //    CreateRequestFile();
            //}

            //StockRequestInfos stockInfosFromFile = SerializationHelper.LoadXML<StockRequestInfos>(requestFilePath);
            //if (stockInfosFromFile != null)
            //{
            //    List<StockRequestInfo> listOfStockInfos = stockInfosFromFile.ListOfStockRequests;
            //    List<MarketValueInfo> lstMarketValues = new List<MarketValueInfo>();

            //    Stopwatch sw = new Stopwatch();
            //    //for (int i = 0; i < 9; i++)
            //    //{
            //    //    sw.Restart();
            //    //    foreach (StockRequestInfo stockRequestInfo in listOfStockInfos)
            //    //    {
            //    //        MarketValueInfo marketValue = GetTickerInfo(stockRequestInfo);
            //    //        if (marketValue != null)
            //    //        {
            //    //            lstMarketValues.Add(marketValue);
            //    //        }
            //    //    }
            //    //    sw.Stop();
            //    //    ApplicationLog.Instance.WriteInfo(String.Format("Time for serial call : '{0}'", sw.ElapsedMilliseconds));
            //    //    sw.Restart();

            //    //    Task<List<MarketValueInfo>> t = NewMethod(listOfStockInfos);
            //    //    t.Wait();

            //    //    if (t.Status.Equals(TaskStatus.RanToCompletion))
            //    //    {
            //    //        List<MarketValueInfo> lMarketVAlues = t.Result.OrderBy(x => x.SYMBOL).ToList();
            //    //        sw.Stop();
            //    //        ApplicationLog.Instance.WriteInfo(String.Format("Time for Task call : '{0}'", sw.ElapsedMilliseconds));
            //    //    }
            //    //}

            //    #region Test offline
            //    ///Remember to comment
            //    lstMarketValues = new List<MarketValueInfo>()
            //    {
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0},
            //        new MarketValueInfo() { SYMBOL = "Symbol1", SECDATE = DateTime.Now.Date, PREVIOUSCLOSE = 1.0, OPEN = 2.0, DAYHIGH = 3.0, DAYLOW = 4.0, LASTPRICE = 5.0, CLOSEPRICE = 6.0, AVERAGEPRICE = 7.0, QUANTITYTRADED = 8.0}
            //    };
            //    #endregion

            //    if (lstMarketValues != null && lstMarketValues.Count > 0)
            //    {
            //        //CreateExcel(new MarketValueInfo());
            //        //UpdateExcel(lstMarketValues);
            //        List<EmailUtil> lst = new List<EmailUtil>()
            //        {
            //            new EmailUtil() { ToEmail = "pratik.gaikwad19@gmail.com", ToName = "Pratik Gaikwad" },
            //            new EmailUtil() { ToEmail = "pratik.gaikwad@outlook.in", ToName = "Pratik outlook" },
            //            new EmailUtil() { ToEmail = "pratiktheking@gmail.com", ToName = "Pratik gmail" },
            //        };
            //        //foreach (EmailUtil u in lst)
            //        //{
            //        //    u.Subject = String.Format("Portfolio Excel for - '{0}'", DateTime.Today.Date.ToString("MM-dd-yyyy"));
            //        //    u.Body = "Please find attached portfolio excel sheet for today.";
            //        //    bool b = EmailUtil.Instance.SendEmail(u, strAttchmentFileName: @"C:\PortfoliExcels\07-18-2015.xlsx");
            //        //    ApplicationLog.Instance.WriteInfo(String.Format("Email sent to '{0}' at '{1}'", u.ToEmail, DateTime.Now));
            //        //    Console.WriteLine(String.Format("Email sent to '{0}' at '{1}'", u.ToEmail, DateTime.Now));
            //        //}

            //        Task<bool> taskEmailSent = SendEmailTaskCall(lst);
            //        taskEmailSent.Wait();
            //        if (taskEmailSent.Status.Equals(TaskStatus.RanToCompletion))
            //        {
            //            if (taskEmailSent.Result)
            //                ApplicationLog.Instance.WriteDebug("Mails sent");
            //            else
            //                ApplicationLog.Instance.WriteDebug("Mails not sent");
            //        }
            //        else
            //            ApplicationLog.Instance.WriteDebug("Email sent task was not completed");
            //    }
            //}
            //else
            //    ApplicationLog.Instance.WriteError("Request file not in correct format.");
        }

        private static async Task<bool> SendEmailTaskCall(List<EmailUtil> lst)
        {
            bool bRtnVal = false;
            try
            {
                List<Task<bool>> emailSendTaskList = (from emailUtil in lst select SendEmailTask(emailUtil)).ToList();
                while (emailSendTaskList.Count > 0)
                {
                    Task<Boolean> firstEMailSent = await Task.WhenAny(emailSendTaskList);
                    emailSendTaskList.Remove(firstEMailSent);
                    bRtnVal = await firstEMailSent;
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bRtnVal;
        }

        async static Task<bool> SendEmailTask(EmailUtil email)
        {
            bool bRtnVal = false;
            try
            {
                email.Subject = String.Format("PortFolio Excel for - '{0}'", DateTime.Today.Date.ToString("MM-dd-yyyy"));
                email.Body = "Please find attached portfolio excel sheet for today.";
                bRtnVal = EmailUtil.Instance.SendEmail(email);
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bRtnVal;
        }

        private static async Task<List<MarketValueInfo>> NewMethod(List<StockRequestInfo> listOfStockInfos)
        {
            List<MarketValueInfo> lstRtnVal = new List<MarketValueInfo>();
            List<Task<MarketValueInfo>> taskList = (from stockRequst in listOfStockInfos select GetTickerInfoTask(stockRequst)).ToList();

            while (taskList.Count > 0)
            {
                // Identify the first task that completes.
                Task<MarketValueInfo> firstFinishedTask = await Task.WhenAny(taskList);

                // ***Remove the selected task from the list so that you don't
                // process it more than once.
                taskList.Remove(firstFinishedTask);

                // Await the completed task.
                MarketValueInfo marketValue = await firstFinishedTask;
                lstRtnVal.Add(marketValue);
            }
            return lstRtnVal;
        }

        async static Task<MarketValueInfo> GetTickerInfoTask(StockRequestInfo stockRequestInfo)
        {
            MarketValueInfo rtnVal = null;
            try
            {
                HttpWebRequest req = HttpWebRequest.Create(stockRequestInfo.URL) as HttpWebRequest;
                req.Accept = "*/*";
                req.Host = "nseindia.com";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0";
                req.Method = "GET";
                req.KeepAlive = true;
                String strResult = String.Empty;
                using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
                {
                    using (Stream s = res.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            strResult = sr.ReadToEnd().ToString().Trim();
                            strResult = strResult.Substring(strResult.IndexOf("futLink"));
                            int nt = strResult.IndexOf("</div>");
                            strResult = strResult.Substring(0, nt);
                            strResult = strResult.Substring(strResult.IndexOf("[{"));
                            nt = strResult.IndexOf("}]");
                            strResult = strResult.Substring(2, nt);
                            var jsonLikeString = strResult.Split(new String[] { "\",\"" }, StringSplitOptions.None);
                            Dictionary<String, String> dTemp = new Dictionary<string, string>();
                            foreach (String str in jsonLikeString)
                            {
                                String str1 = str.Replace("\\", String.Empty);
                                str1 = str1.Replace("\"", String.Empty);
                                dTemp.Add(str1.Split(':')[0].Trim().ToUpper(), str1.Split(':')[1].Trim());
                            }

                            if (dTemp.Count > 0)
                            {
                                rtnVal = new MarketValueInfo();
                                PropertyInfo[] props = rtnVal.GetType().GetProperties();
                                foreach (PropertyInfo prop in props)
                                {
                                    try
                                    {
                                        Object propValue = null;

                                        TypeConverter typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);
                                        try
                                        {
                                            propValue = typeConverter.ConvertFrom(dTemp[prop.Name]);
                                        }
                                        catch
                                        {
                                            if (typeConverter.CanConvertTo(prop.PropertyType) && dTemp.ContainsKey(prop.Name))
                                            {
                                                propValue = typeConverter.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, dTemp[prop.Name], prop.PropertyType);
                                            }
                                        }

                                        rtnVal.GetType().GetProperty(prop.Name).SetValue(rtnVal, propValue, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        ApplicationLog.Instance.WriteException(ex);
                                    }
                                }
                                #region keeping for ref only
                                //String previousClose = dTemp["previousClose"];
                                //String open = dTemp["open"];
                                //String dayHigh = dTemp["dayHigh"];
                                //String dayLow = dTemp["dayLow"];
                                //String lastPrice = dTemp["lastPrice"];
                                //String closePrice = dTemp["closePrice"];
                                //String averagePrice = dTemp["averagePrice"];
                                //String quantityTraded = dTemp["quantityTraded"];
                                #endregion
                            }
                            else
                                throw new Exception(String.Format("Webrequest failed for symbol: {0}", stockRequestInfo.Ticker));
                        }
                    }
                }
                Console.WriteLine(String.Format("Symbol read done: '{0}'", stockRequestInfo.Ticker));
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteError(String.Format("Error in ticker : '{0}'", stockRequestInfo.Ticker));
                ApplicationLog.Instance.WriteException(ex);
            }
            return rtnVal;
        }

        private static bool CreateExcel(MarketValueInfo marketValue)
        {
            bool bRtnVal = false;
            Application excelApp = null;
            Workbook workBook = null;
            Worksheet workSheet = null;
            try
            {
                var fileName = String.Format("{0}{1}.xlsx", PortfoliExcelPath, excelFileName);
                if (!File.Exists(fileName))
                {
                    excelApp = new Application();
                    workBook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                    workSheet = workBook.Sheets[1];
                    PropertyInfo[] properties = marketValue.GetType().GetProperties();

                    int ncol = 1;
                    foreach (PropertyInfo property in properties)
                    {
                        workSheet.Cells[1, ncol] = property.Name;
                        workSheet.Cells[1, ncol].Font.Bold = true;
                        ncol++;
                    }
                    //To freeze first rwo
                    workSheet.Application.ActiveWindow.SplitRow = 1;
                    workSheet.Application.ActiveWindow.FreezePanes = true;

                    excelApp.DisplayAlerts = false;
                    workBook.Save();
                    excelApp.ActiveWorkbook.SaveAs(fileName);
                    workBook.Close(0);
                    excelApp.Quit();
                }
                bRtnVal = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (workSheet != null)
                    Marshal.ReleaseComObject(workSheet);
                if (workBook != null)
                    Marshal.ReleaseComObject(workBook);
                if (excelApp != null)
                    Marshal.ReleaseComObject(excelApp);
            }
            return bRtnVal;
        }

        private static bool UpdateExcel(List<MarketValueInfo> lstMarketValue)
        {
            bool bRtnVal = false;
            Workbook workBook = null;
            Application excelApp = null;
            Worksheet workSheet = null;
            Range sourceRange = null;
            try
            {
                var fileName = String.Format(@"{0}{1}.xlsx", PortfoliExcelPath, excelFileName);
                excelApp = new Application();
                Workbooks workBooks = excelApp.Workbooks;
                workBook = excelApp.Workbooks.Open(fileName, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workSheet = workBook.Worksheets[1];
                int nColumns = workSheet.UsedRange.Columns.Count;
                int nRows = workSheet.UsedRange.Rows.Count;
                foreach (MarketValueInfo marketValue in lstMarketValue)
                {
                    try
                    {
                        for (int i = 1; i <= nColumns; i++)
                        {
                            var columnName = (workSheet.Cells[1, i] as Range).Value as String;
                            var value = marketValue.GetType().GetProperty(columnName).GetValue(marketValue);
                            workSheet.Cells[nRows + 1, i] = value;
                            sourceRange = workSheet.Cells[nRows, i];
                            if (nRows > 1)
                            {
                                sourceRange.Copy(Type.Missing);
                                var hasFormula = sourceRange.HasFormula;
                                Range destinationRange = workSheet.Cells[nRows + 1, i];
                                destinationRange.PasteSpecial(XlPasteType.xlPasteFormats, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                                if (hasFormula)
                                    destinationRange.PasteSpecial(XlPasteType.xlPasteFormulas, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                            }
                        }
                        nRows++;
                        Console.WriteLine(String.Format("Symbol write done: '{0}'", marketValue.SYMBOL));
                    }
                    catch (Exception ex)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("Excel update failed for {0}", marketValue.SYMBOL));
                        ApplicationLog.Instance.WriteException(ex);
                    }
                }

                //To autofit the columns.
                workSheet.Columns.AutoFit();

                excelApp.DisplayAlerts = false;
                workBook.Save();
                workBook.Close(0);
                excelApp.Quit();
                bRtnVal = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (workSheet != null)
                    Marshal.ReleaseComObject(workSheet);
                if (workBook != null)
                    Marshal.ReleaseComObject(workBook);
                if (excelApp != null)
                    Marshal.ReleaseComObject(excelApp);
            }
            return bRtnVal;
        }

        private static void CreateRequestFile()
        {
            try
            {
                String strRawRequest = File.ReadAllText(rowRequestFilePath);
                String[] strStockRequestInfos = strRawRequest.Split(';');
                StockRequestInfos o_StockRequestInfos = new StockRequestInfos();
                o_StockRequestInfos.ListOfStockRequests = new List<StockRequestInfo>();
                foreach (String strTempRequest in strStockRequestInfos)
                {
                    if (!String.IsNullOrEmpty(strTempRequest))
                    {
                        String[] t = strTempRequest.Split(' ');
                        StockRequestInfo obj = new StockRequestInfo();
                        obj.Ticker = t[0].Trim();
                        obj.URL = t[1].Trim();
                        o_StockRequestInfos.ListOfStockRequests.Add(obj);
                    }
                }
                if (o_StockRequestInfos.ListOfStockRequests != null && o_StockRequestInfos.ListOfStockRequests.Count > 0)
                {
                    o_StockRequestInfos.ListOfStockRequests = o_StockRequestInfos.ListOfStockRequests.OrderBy(x => x.Ticker).ToList();
                    SerializationHelper.WriteXML<StockRequestInfos>(o_StockRequestInfos, requestFilePath);
                }
                else
                    ApplicationLog.Instance.WriteError("List of request is not created. please check raw file");
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        private static MarketValueInfo GetTickerInfo(StockRequestInfo stockRequestInfo)
        {
            MarketValueInfo rtnVal = null;
            try
            {
                HttpWebRequest req = HttpWebRequest.Create(stockRequestInfo.URL) as HttpWebRequest;
                req.Accept = "*/*";
                req.Host = "nseindia.com";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0";
                req.Method = "GET";
                req.KeepAlive = true;
                String strResult = String.Empty;
                using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
                {
                    using (Stream s = res.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            strResult = sr.ReadToEnd().ToString().Trim();
                            strResult = strResult.Substring(strResult.IndexOf("futLink"));
                            int nt = strResult.IndexOf("</div>");
                            strResult = strResult.Substring(0, nt);
                            strResult = strResult.Substring(strResult.IndexOf("[{"));
                            nt = strResult.IndexOf("}]");
                            strResult = strResult.Substring(2, nt);
                            var jsonLikeString = strResult.Split(new String[] { "\",\"" }, StringSplitOptions.None);
                            Dictionary<String, String> dTemp = new Dictionary<string, string>();
                            foreach (String str in jsonLikeString)
                            {
                                String str1 = str.Replace("\\", String.Empty);
                                str1 = str1.Replace("\"", String.Empty);
                                dTemp.Add(str1.Split(':')[0].Trim().ToUpper(), str1.Split(':')[1].Trim());
                            }

                            if (dTemp.Count > 0)
                            {
                                rtnVal = new MarketValueInfo();
                                PropertyInfo[] props = rtnVal.GetType().GetProperties();
                                foreach (PropertyInfo prop in props)
                                {
                                    try
                                    {
                                        Object propValue = null;

                                        TypeConverter typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);
                                        try
                                        {
                                            propValue = typeConverter.ConvertFrom(dTemp[prop.Name]);
                                        }
                                        catch
                                        {
                                            if (typeConverter.CanConvertTo(prop.PropertyType) && dTemp.ContainsKey(prop.Name))
                                            {
                                                propValue = typeConverter.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, dTemp[prop.Name], prop.PropertyType);
                                            }
                                        }

                                        rtnVal.GetType().GetProperty(prop.Name).SetValue(rtnVal, propValue, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        ApplicationLog.Instance.WriteException(ex);
                                    }
                                }
                                #region keeping for ref only
                                //String previousClose = dTemp["previousClose"];
                                //String open = dTemp["open"];
                                //String dayHigh = dTemp["dayHigh"];
                                //String dayLow = dTemp["dayLow"];
                                //String lastPrice = dTemp["lastPrice"];
                                //String closePrice = dTemp["closePrice"];
                                //String averagePrice = dTemp["averagePrice"];
                                //String quantityTraded = dTemp["quantityTraded"];
                                #endregion
                            }
                            else
                                throw new Exception(String.Format("Webrequest failed for symbol: {0}", stockRequestInfo.Ticker));
                        }
                    }
                }
                Console.WriteLine(String.Format("Symbol read done: '{0}'", stockRequestInfo.Ticker));
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteError(String.Format("Error in ticker : '{0}'", stockRequestInfo.Ticker));
                ApplicationLog.Instance.WriteException(ex);
            }
            return rtnVal;
        }
    }
}
