using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyHut.Controllers
{
    public class ElectricianController : Controller
    {
        // GET: Electrician
        #region Variables
        bool bIsSaveRequestSuccessful = false;
        #endregion
        public ActionResult EnterData(int nServiceId)
        {
            ViewBag.ServiceId = nServiceId;
            ViewBag.bIsSaveRequestSuccessful = bIsSaveRequestSuccessful;
            return View();
        }

        [HttpPost]
        public ActionResult SaveQuoteRequest(int nServiceId, GetQuoteInfo viewModel, FormCollection fCollection)
        {
            String strActionName = String.Empty;
            String strControllerName = String.Empty;
            if (ModelState.IsValid)
            {
                viewModel.ServiceId = nServiceId;
                ServiceInteractiveController controller = new ServiceInteractiveController();
                bool bRtnval = controller.SetCustomerRequestInfo(viewModel);
                if (!bRtnval)
                {
                    strActionName = "Index";
                    strControllerName = "Error";
                }
                else
                {
                    strActionName = "Electrician";
                    strControllerName = "EnterData";
                    bIsSaveRequestSuccessful = true;
                }
            }
            else
            {
                strControllerName = "Electrician";
                strActionName = "EnterData";
            }
            return RedirectToAction(strActionName, strControllerName);
        }
    }
}