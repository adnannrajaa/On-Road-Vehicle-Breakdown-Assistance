using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleAssistance.ViewModels.Customer;

namespace VehicleAssistance.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAvailableMechanics()
        {
            var data = CustomerViewModel.GetAllMechanics();
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetRequestedMechanics()
        {
            int currentUserId = Convert.ToInt32(Session["currentUserId"].ToString());
            var data = CustomerViewModel.GetAllRequestedMechanics(currentUserId);
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendRequestToMechanic(int mechanicId)
        {
            int currentUserId = Convert.ToInt32(Session["currentUserId"].ToString());
            bool status = CustomerViewModel.SendRequest(currentUserId, mechanicId);
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CustomerFeedback(int RequestId, string feeback)
        {
            int currentUserId = Convert.ToInt32(Session["currentUserId"].ToString());
            bool status = CustomerViewModel.SaveCustomerFeedback(RequestId, currentUserId , feeback);
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);
        }
        
    }
}