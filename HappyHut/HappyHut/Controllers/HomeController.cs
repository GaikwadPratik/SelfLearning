using BussinessObjects;
using LogUtils;
using System;
using System.Web.Mvc;

namespace HappyHut.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Main()
        {
            ServiceInteractiveController serviceController = new ServiceInteractiveController();
            String jsonCities = serviceController.GetCities();
            ViewData["jsonCities"] = jsonCities;
            ViewBag.ShowHomeHeaders = true;
            return View();
        }

        public String GetAreasInCity(String strCityId)
        {
            int nCityId = 0;
            int.TryParse(strCityId, out nCityId);
            ServiceInteractiveController serviceController = new ServiceInteractiveController();
            String jsonAreaInCity = serviceController.GetAreasInCity(nCityId);
            return jsonAreaInCity;
        }

        public String GetServicesInArea(String strAreaId)
        {
            int nAreaId = 0;
            int.TryParse(strAreaId, out nAreaId);
            ServiceInteractiveController serviceController = new ServiceInteractiveController();
            String jsonServiceInArea = serviceController.GetServicesInArea(nAreaId);
            return jsonServiceInArea;
        }

        public ActionResult ShowViewByService(String hdnCityAreaService)
        {
            int nServiceId = 0;
            int nAreaId = 0;
            String strActionName = String.Empty;
            String strControllerName = String.Empty;
            ServiceInAreaInfo serviceInAreaInfo = null;

            if (int.TryParse(hdnCityAreaService.Split(';')[1], out nAreaId) && int.TryParse(hdnCityAreaService.Split(';')[2], out nServiceId))
            {
                ServiceInteractiveController serviceController = new ServiceInteractiveController();
                serviceInAreaInfo = serviceController.GetServiceInArea(nAreaId, nServiceId);

                if (serviceInAreaInfo != null)
                {
                    if (serviceInAreaInfo.ServiceName.ToLower().Contains("pest"))
                    {
                        strActionName = "EnterData";
                        strControllerName = "PestControl";
                    }
                    else if(serviceInAreaInfo.ServiceName.ToLower().Contains("electri"))
                    {
                        strActionName = "EnterData";
                        strControllerName = "Electrician";
                    }
                }
                else
                    ApplicationLog.Instance.WriteError("Error in getting Service in area info for redirection");
            }

            if (!String.IsNullOrEmpty(strActionName) && !String.IsNullOrEmpty(strControllerName))
                return RedirectToAction(strActionName, strControllerName, new { nServiceId = serviceInAreaInfo.ServiceInAreaId });
            else
            {
                strActionName = "Index";
                strControllerName = "Error";
                return RedirectToAction(strActionName, strControllerName);
            }
        }
    }
}
