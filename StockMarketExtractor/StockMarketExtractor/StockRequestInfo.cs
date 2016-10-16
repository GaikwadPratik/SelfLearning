using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketExtractor
{
    public class StockRequestInfo
    {
        public String Ticker { get; set; }
        public String URL { get; set; }
    }

    public class StockRequestInfos
    {
        public List<StockRequestInfo> ListOfStockRequests { get; set; }
    }

    public class MarketValueInfo
    {
        public String SYMBOL { get; set; }
        public DateTime SECDATE { get; set; }
        public Double PREVIOUSCLOSE  { get; set; }
        public Double OPEN  { get; set; }
        public Double DAYHIGH { get; set; }
        public Double DAYLOW { get; set; }
        public Double LASTPRICE { get; set; }
        public Double CLOSEPRICE { get; set; }
        public Double AVERAGEPRICE { get; set; }
        public Double QUANTITYTRADED { get; set; }
    }
}
