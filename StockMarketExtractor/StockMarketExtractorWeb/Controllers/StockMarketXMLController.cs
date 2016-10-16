using LogUtils;
using SerializationExtensionUtils;
using StockMarketExtractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockMarketExtractorWeb.Controllers
{
    public class StockMarketXMLController : Controller
    {
        static String requestFilePath = @"C:\PortfoliExcels\files\StockRequest.xml";
        static String rowRequestFilePath = @"C:\PortfoliExcels\files\RawRequestInfo.txt";
        static String strStatusFile = @"C:\PortfoliExcels\files\Status.xml";
        // GET: StockMarketXML
        public ActionResult LoadList()
        {
            StockRequestInfos stockInfosFromFile = SerializationHelper.LoadXML<StockRequestInfos>(requestFilePath);
            if (stockInfosFromFile != null && stockInfosFromFile.ListOfStockRequests != null && stockInfosFromFile.ListOfStockRequests.Count > 0)
            {
                ViewBag.ListOfStocksRequested = stockInfosFromFile.ListOfStockRequests;
            }
            else
                ApplicationLog.Instance.WriteWarning("Something wrong with reading request xml file");
            return View();
        }
    }
}