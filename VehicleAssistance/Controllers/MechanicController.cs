using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleAssistance.ViewModels.Mechanics;

namespace VehicleAssistance.Controllers
{
    public class MechanicController : Controller
    {
        // GET: Mechanic
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCustomerRequests()
        {
            int currentUserId =  Convert.ToInt32(Session["currentUserId"].ToString());
            var data = MechanicsViewModel.GetCustomerRequest(currentUserId);
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        
    }
}