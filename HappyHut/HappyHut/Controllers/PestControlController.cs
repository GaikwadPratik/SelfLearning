using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyHut.Controllers
{
    public class PestControlController : Controller
    {
        // GET: PestControl
        public ActionResult EnterData(int nServiceId)
        {
            ViewBag.ServiceId = nServiceId;
            return View();
        }

        [HttpPost]
        public ActionResult SaveQuoteRequest(int nServiceId, GetQuoteInfo viewModel, FormCollection fCollection)
        {
            if (ModelState.IsValid)
            {
                viewModel.AdditionalInfo = !String.IsNullOrEmpty(viewModel.AdditionalInfo) ? String.Format("{0},{1}", viewModel.AdditionalInfo, fCollection["ddnNumberOfBedRooms"]) : fCollection["ddnNumberOfBedRooms"];
                viewModel.ServiceId = nServiceId;
                ServiceInteractiveController controller = new ServiceInteractiveController();
                controller.SetCustomerRequestInfo(viewModel);
            }
            return View();
        }
    }
}