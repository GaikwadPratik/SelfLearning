using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RTKExtraction
{
    class Program
    {
        static string strOutfileName = "output.xlsx";

        static void Main(string[] args)
        {
            string _strInFileName = "Book1.csv";
            if (File.Exists(_strInFileName))
            {
                string[] _strCompnayNames = File.ReadAllLines(_strInFileName);
                List<WastageValues> _lst = new List<WastageValues>();
                Dictionary<string, Dictionary<int, WastageValues>> _dic1 = new Dictionary<string, Dictionary<int, WastageValues>>();
                int _i = 1;
                foreach (string _strCompnayName in _strCompnayNames)
                {
                    string _strBaseLink = $"http://ec2-52-202-150-226.compute-1.amazonaws.com/tri/tri.php?database=tri&reptype=f&reporting_year=2014&first_year_range=1987&last_year_range=2014&facility_name=&parent={_strCompnayName.Replace(" ", "+")}&combined_name=&parent_duns=&facility_id=&city=&county=&state=&zip=&district=&naics=&primall=&chemcat=&corechem=n&casno=&casno2=&chemname=&detail=-1&datype=X&rsei=y&sortp=D";
                    HttpWebRequest req = WebRequest.Create(_strBaseLink) as HttpWebRequest;
                    req.Accept = "*/*";
                    req.Host = "ec2-52-202-150-226.compute-1.amazonaws.com";
                    req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:52.0) Gecko/20100101 Firefox/52.0";
                    req.Method = "GET";
                    req.KeepAlive = true;
                    string strResult = string.Empty;
                    using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
                    {
                        using (Stream s = res.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(s))
                            {
                                strResult = sr.ReadToEnd().ToString().Trim();
                                if (strResult.Contains("<error>No records found</error>"))
                                    continue;

                                Dictionary<int, WastageValues> _dic = new Dictionary<int, WastageValues>();
                                XmlSerializer ser = new XmlSerializer(typeof(RTKNetSearchResults));
                                using (TextReader stream = new StringReader(strResult))
                                {
                                    RTKNetSearchResults _searchResult = (RTKNetSearchResults)ser.Deserialize(stream);
                                    if (_searchResult == null) throw new NullReferenceException("Invalid xml format");
                                    bool _bHasData = false;
                                    foreach (ReportingYear _reportingYear in _searchResult.Data.Record.ReleaseTrend.ReportingYears)
                                    {
                                        _bHasData = true;
                                        WastageValues _values = null;
                                        _dic.TryGetValue(_reportingYear.Year, out _values);
                                        if (_values == null)
                                            _values = new WastageValues();
                                        _values.Release = _reportingYear.Value;
                                        _dic[_reportingYear.Year] = _values;
                                    }

                                    foreach (ReportingYear _reportingYear in _searchResult.Data.Record.WasteTrend.ReportingYears)
                                    {
                                        _bHasData = true;
                                        WastageValues _values = null;
                                        _dic.TryGetValue(_reportingYear.Year, out _values);
                                        if (_values == null)
                                            _values = new WastageValues();
                                        _values.Waste = _reportingYear.Value;
                                        _dic[_reportingYear.Year] = _values;
                                    }

                                    foreach (ReportingYear _reportingYear in _searchResult.Data.Record.RseiScoreTrend.ReportingYears)
                                    {
                                        _bHasData = true;
                                        WastageValues _values = null;
                                        _dic.TryGetValue(_reportingYear.Year, out _values);
                                        if (_values == null)
                                            _values = new WastageValues();
                                        _values.Score = _reportingYear.Value;
                                        _dic[_reportingYear.Year] = _values;
                                    }

                                    if (_bHasData)
                                    {
                                        _dic1.Add(_strCompnayName, _dic);

                                        Console.WriteLine($"{_i}. {_strCompnayName} pulled!");
                                        _i++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (_dic1.Count > 0)
                {
                    strOutfileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strOutfileName);
                    Dictionary<int, int> _dicYear = CreateExcel();
                    UpdateExcel(_dicYear, _dic1);
                }
                else
                    Console.WriteLine("No data to process");
            }
            else
                Console.WriteLine($"Input CSV file {_strInFileName}for company names not found");
        }

        private static bool UpdateExcel(Dictionary<int, int> dicYear, Dictionary<string, Dictionary<int, WastageValues>> _dic1)
        {
            bool bRtnVal = false;
            Workbook workBook = null;
            Application excelApp = null;
            Worksheet workSheet = null;
            try
            {
                excelApp = new Application();
                Workbooks workBooks = excelApp.Workbooks;
                workBook = excelApp.Workbooks.Open(strOutfileName, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workSheet = workBook.Worksheets[1];
                int nColumns = workSheet.UsedRange.Columns.Count;
                int nRows = workSheet.UsedRange.Rows.Count;
                int _nCurrentRow = 3;
                int _i = 1;
                foreach (KeyValuePair<string, Dictionary<int, WastageValues>> kvp in _dic1)
                {
                    workSheet.Cells[_nCurrentRow, 1] = kvp.Key;
                    int i = 2;
                    foreach (KeyValuePair<int, WastageValues> kvp1 in kvp.Value)
                    {
                        i = dicYear[kvp1.Key];
                        WastageValues _value = kvp1.Value;

                        workSheet.Cells[_nCurrentRow, i] = _value.Release;
                        workSheet.Cells[_nCurrentRow, ++i] = _value.Waste;
                        workSheet.Cells[_nCurrentRow, ++i] = _value.Score;
                        i++;
                    }
                    Console.WriteLine($"{_i}. {kvp.Key} pushed!");
                    _i++;
                    _nCurrentRow++;
                }

                //To aut
                workSheet.Columns.AutoFit();

                excelApp.DisplayAlerts = false;
                workBook.Save();
                workBook.Close(0);
                excelApp.Quit();
                Console.WriteLine($"Excel file update completed at '{DateTime.Now}'");
                bRtnVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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

        private static Dictionary<int, int> CreateExcel()
        {
            Dictionary<int, int> bRtnVal = new Dictionary<int, int>();
            Application excelApp = null;
            Workbook workBook = null;
            Worksheet workSheet = null;
            try
            {
                if (File.Exists(strOutfileName))
                    File.Delete(strOutfileName);
                excelApp = new Application();
                workBook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                workSheet = workBook.Sheets[1];

                int _nCellRange = 2;
                for (int _nFinalYear = DateTime.Today.Year, _nStartYear = 1987; _nStartYear <= _nFinalYear; _nStartYear++)
                {
                    bRtnVal.Add(_nStartYear, _nCellRange);
                    workSheet.Cells[1, _nCellRange] = _nStartYear;
                    int _nEndCellRange = _nCellRange + 2;
                    Range range = workSheet.Range[workSheet.Cells[1, _nCellRange], workSheet.Cells[1, _nEndCellRange]];
                    range.Merge();
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    _nCellRange = _nEndCellRange + 1;
                }

                workSheet.Cells[2, 1] = "Company Name";
                int _nLastCell = _nCellRange;
                _nCellRange = 2;
                for (; _nCellRange <= _nLastCell; _nCellRange++)
                {
                    workSheet.Cells[2, _nCellRange] = "Releases (lbs)";
                    workSheet.Cells[2, ++_nCellRange] = "Waste (lbs)";
                    workSheet.Cells[2, ++_nCellRange] = "RSEI Score";
                }

                workSheet.Range[workSheet.Cells[2, 2], workSheet.Cells[2, _nCellRange]].Font.Bold = true;

                //To freeze first rwo
                workSheet.Application.ActiveWindow.SplitRow = 2;
                workSheet.Application.ActiveWindow.FreezePanes = true;
                workSheet.Columns.AutoFit();

                excelApp.DisplayAlerts = false;
                workBook.Save();
                excelApp.ActiveWorkbook.SaveAs(strOutfileName);
                workBook.Close(0);
                excelApp.Quit();
                Console.WriteLine($"Excel file creation completed at '{DateTime.Now}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
    }

    public class WastageValues
    {
        public double Release { get; set; }
        public double Waste { get; set; }
        public double Score { get; set; }
    }

    public class RTKNetSearchResults
    {
        [XmlElement(ElementName = "search_criteria")]
        public List<SearchCriterion> SearchCriteria { get; set; }

        [XmlElement(ElementName = "data")]
        public Data Data { get; set; }
    }

    public class SearchCriterion
    {
        [XmlAttribute(AttributeName = "field")]
        public string Key { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    public class Data
    {
        [XmlAttribute(AttributeName = "compiled_from_government_data_last_released_on")]
        public DateTime ReleasedOn { get; set; }

        [XmlAttribute(AttributeName = "database")]
        public string DataBase { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "max_records")]
        public double MaxRecords { get; set; }

        [XmlElement(ElementName = "record")]
        public Record Record { get; set; }
    }

    public class Record
    {
        [XmlAttribute(AttributeName = "detail")]
        public string Detail { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "database")]
        public string DataBase { get; set; }

        //[XmlElement(ElementName = "totals")]
        //public Total Totals { get; set; }

        //[XmlElement(ElementName = "release_categories")]
        //public ReleaseCategories ReleaseCategories { get; set; }

        //[XmlElement(ElementName = "waste_categories")]
        //public WasteCategories WasteCategories { get; set; }

        //[XmlElement(ElementName = "top_cities")]
        //public Cities Cities { get; set; }

        //[XmlElement(ElementName = "top_districts")]
        //public Districts Districts { get; set; }

        //[XmlElement(ElementName = "top_parent_companies")]
        //public TopParentCompanies TopParentCompanies { get; set; }

        [XmlElement(ElementName = "release_trend")]
        public ReleaseTrend ReleaseTrend { get; set; }

        [XmlElement(ElementName = "waste_trend")]
        public WasteTrend WasteTrend { get; set; }

        [XmlElement(ElementName = "rsei_score_trend")]
        public RseiScoreTrend RseiScoreTrend { get; set; }
    }

    public class RseiScoreTrend
    {
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "reporting_year")]
        public List<ReportingYear> ReportingYears { get; set; }
    }

    public class WasteTrend
    {
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "reporting_year")]
        public List<ReportingYear> ReportingYears { get; set; }
    }

    public class ReleaseTrend
    {
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "reporting_year")]
        public List<ReportingYear> ReportingYears { get; set; }
    }

    public class ReportingYear
    {
        [XmlAttribute(AttributeName = "year")]
        public int Year { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    public class Total
    {
        [XmlElement(ElementName = "total_releases")]
        public double TotalReleases { get; set; } = 5758923294.3849;

        [XmlElement(ElementName = "total_waste")]
        public double TotalWaste { get; set; } = 5759637377.8199;

        [XmlElement(ElementName = "number_of_facilities")]
        public double NumberOfFacilities { get; set; } = 3;

        [XmlElement(ElementName = "number_of_submissions")]
        public double NumberOfSubmissions { get; set; } = 119;

        [XmlElement(ElementName = "number_of_form_a_submissions")]
        public double NumberOfFormASubmissions { get; set; } = 6;

        [XmlElement(ElementName = "rsei_score_over_year_range")]
        public double RseiScoreOverYearRange { get; set; } = 737.958593;

        [XmlElement(ElementName = "average_percent_of_total_rsei_score_for_year_range")]
        public double AveragePercentOfTotalRseiScoreForYearRange { get; set; } = 9.0599113515003;
    }

    public class ReleaseCategories
    {
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; } = "total releases by type of release";

        [XmlElement(ElementName = "onsite_fugitive_air")]
        public double OnSiteFugitiveAir { get; set; } = 1940362.4;

        [XmlElement(ElementName = "onsite_stack_air")]
        public double OnsiteStackAir { get; set; } = 11189.3129103;

        [XmlElement(ElementName = "onsite_water")]
        public double OnsiteWater { get; set; } = 20114.086;

        [XmlElement(ElementName = "onsite_land_disposal")]
        public double OnsiteLandDisposal { get; set; } = 5756946353.03;

        [XmlElement(ElementName = "offsite_transfers_to_disposal")]
        public double OffsiteTransfersToDisposal { get; set; } = 3775.556;

        [XmlElement(ElementName = "Form_A_midpoint_estimates")]
        public double FormAMidpointEstimates { get; set; } = 1500;
    }

    public class WasteCategories
    {
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; } = "total wastes by type of waste";

        [XmlElement(ElementName = "recycled_onsite")]
        public double RecycledOnsite { get; set; } = 146274;

        [XmlElement(ElementName = "recycled_offsite")]
        public double RecycledOffsite { get; set; } = 196784.795;

        [XmlElement(ElementName = "treated_onsite")]
        public double TreatedOnsite { get; set; } = 371025.0000000;

        [XmlElement(ElementName = "released_onsite_or_disposed_offsite")]
        public double ReleasedOnsiteOrDisposedOffsite { get; set; } = 5758921794.0249;

        [XmlElement(ElementName = "Form_A_midpoint_estimates")]
        public double FormAMidpointEstimates { get; set; } = 1500;
    }

    public class Cities
    {
        [XmlAttribute(AttributeName = "ranked_by")]
        public string RankedBy { get; set; }

        [XmlAttribute(AttributeName = "maximum_shown")]
        public int MaximumShown { get; set; }

        [XmlElement(ElementName = "city")]
        public List<City> City { get; set; }
    }

    public class City
    {
        [XmlAttribute(AttributeName = "rank")]
        public int Rank { get; set; }

        [XmlAttribute(AttributeName = "total_onsite_releases")]
        public double TotalOnsiteReleases { get; set; }

        [XmlText]
        public string Name { get; set; }

    }

    public class Districts
    {
        [XmlAttribute(AttributeName = "ranked_by")]
        public string RankedBy { get; set; }

        [XmlAttribute(AttributeName = "maximum_shown")]
        public int MaximumShown { get; set; }

        [XmlElement(ElementName = "Congressional_district")]
        public List<District> District { get; set; }
    }

    public class District
    {
        [XmlAttribute(AttributeName = "rank")]
        public int Rank { get; set; }

        [XmlAttribute(AttributeName = "total_onsite_releases")]
        public double TotalOnsiteReleases { get; set; }

        [XmlText]
        public string Name { get; set; }
    }

    public class TopParentCompanies
    {
        [XmlAttribute(AttributeName = "ranked_by")]
        public string RankedBy { get; set; }

        [XmlAttribute(AttributeName = "maximum_shown")]
        public int MaximumShown { get; set; }

        [XmlElement(ElementName = "parent_company")]
        public List<ParentCompany> ParentCompany { get; set; }
    }

    public class ParentCompany
    {
        [XmlAttribute(AttributeName = "rank")]
        public int Rank { get; set; }

        [XmlAttribute(AttributeName = "total_releases")]
        public double TotalRelease { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
