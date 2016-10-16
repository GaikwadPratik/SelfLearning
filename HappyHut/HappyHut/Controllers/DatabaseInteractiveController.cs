using BussinessObjects;
//using HappyHut.HappyHutServices;
using HappyHutMiddleTier;
using LogUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyHut.Controllers
{
    /// <summary>
    /// Please use this controller to do an SVC call only.
    /// Don't create any view from this controller.
    /// </summary>
    public class ServiceInteractiveController : Controller
    {
        DatabaseClass client = null;

        public ServiceInteractiveController()
        {
            client = new DatabaseClass();
        }
        // GET: ServiceInteractive
        public String GetCities()
        {
            String jsonCities = String.Empty;
            try
            {
                var cityArray = client.GetCityList();
                jsonCities = JsonConvert.SerializeObject(cityArray, Formatting.None, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return jsonCities;
        }

        public String GetAreasInCity(int nCityId)
        {
            String jsonAreaInCity = String.Empty;
            try
            {
                var areaInCity = client.GetAreasInCity(nCityId);
                jsonAreaInCity = JsonConvert.SerializeObject(areaInCity, Formatting.None, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return jsonAreaInCity;
        }

        public String GetServicesInArea(int nAreaId)
        {
            String jsonServiceInArea = String.Empty;
            try
            {
                var serviceInArea = client.GetServicesInArea(nAreaId);
                jsonServiceInArea = JsonConvert.SerializeObject(serviceInArea, Formatting.None, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return jsonServiceInArea;
        }

        public bool SetCustomerRequestInfo(GetQuoteInfo request)
        {
            bool bFlag = false;
            try
            {
                bFlag = client.SetCustomerRequestInfo(request);
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bFlag;
        }

        public ServiceInAreaInfo GetServiceInArea(int nAreaId, int nServiceId)
        {
            ServiceInAreaInfo inf = null;
            try
            {
                inf = client.GetServiceInArea(nAreaId, nServiceId);
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return inf;
        }
    }
}