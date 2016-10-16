using LogUtils;
using Microsoft.Office.Interop.Excel;
using SerializationExtensionUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketExtractor
{
    class OldCode
    {
        static String requestFilePath = @"files\StockRequest.xml";
        static String rowRequestFilePath = @"files\RawRequestInfo.txt";
        static String PortfoliExcelPath = @"C:\PortfoliExcels\";
        void Main()
        {
            if (!File.Exists(requestFilePath))
            {
                CreateRequestFile();
            }

            StockRequestInfos stockInfosFromFile = SerializationHelper.LoadXML<StockRequestInfos>(requestFilePath);
            if (stockInfosFromFile != null)
            {
                List<StockRequestInfo> listOfStockInfos = stockInfosFromFile.ListOfStockRequests;
                foreach (StockRequestInfo stockRequestInfo in listOfStockInfos)
                {
                    MarketValueInfo marketValue = GetTickerInfo(stockRequestInfo);
                    if (marketValue != null)
                    {
                        CreateExcel(marketValue);
                        if (!UpdateExcel(marketValue))
                            ApplicationLog.Instance.WriteError(String.Format("Excel update failed for {0}", stockRequestInfo.Ticker));
                        Console.WriteLine(String.Format("Symbole done {0}", marketValue.SYMBOL));
                    }
                }
            }
            else
                ApplicationLog.Instance.WriteError("Request file not in correct format.");
        }
        private static bool CreateExcel(MarketValueInfo marketValue)
        {
            bool bRtnVal = false;
            Application excelApp = null;
            Workbook workBook = null;
            Worksheet workSheet = null;
            try
            {
                var fileName = String.Format("{0}{1}.xlsx", PortfoliExcelPath, marketValue.SYMBOL);
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
                        ncol++;
                    }
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

        private static bool UpdateExcel(MarketValueInfo marketValue)
        {
            bool bRtnVal = false;
            Workbook workBook = null;
            Application excelApp = null;
            Worksheet workSheet = null;
            Range sourceRange = null;
            try
            {
                var fileName = String.Format(@"{0}{1}.xlsx", PortfoliExcelPath, marketValue.SYMBOL);
                excelApp = new Application();
                Workbooks workBooks = excelApp.Workbooks;
                workBook = excelApp.Workbooks.Open(fileName, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workSheet = workBook.Worksheets[1];
                int nColumns = workSheet.UsedRange.Columns.Count;
                int nRows = workSheet.UsedRange.Rows.Count;

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
